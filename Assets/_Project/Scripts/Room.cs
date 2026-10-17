using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Room : MonoBehaviour
{
    private PlayableDirector _director;

    [Header("Laser Settings")]
    [SerializeField] private List<BaseLaserController> _lasersList;


    [Header("Checkpoint References")]
    [SerializeField] private CheckPoint _checkPoint;


    [Header("Trigger References")]
    [SerializeField] private RoomEnterTrigger _enterTrigger;
    [SerializeField] private RoomPassTrigger _passTrigger;


    [Header("Barrier References")]
    [SerializeField] private Barrier _barrier;


    private bool _hasPlayedIntro = false;
    public RoomPassTrigger PassTrigger => _passTrigger;
    private void Awake()
    {
        _director = GetComponent<PlayableDirector>();
    }
    public void Initialize()
    {
        foreach (var laser in _lasersList)
        {
            laser.SetLaserStatus(LaserStatus.Standby);
        }

        _enterTrigger.gameObject.SetActive(false);

        if (_checkPoint != null && _checkPoint.Trigger != null)
            _checkPoint.Trigger.OnPlayerEnterCheckPoint += OnPlayerEnterCheckPoint;
    }
    private void OnEnable()
    {
        _enterTrigger.OnPlayerEnteredRoom += PlayerEnteredRoom;
        _passTrigger.OnPlayerPassedRoom += PlayerPassedRoom;
        

        if(_director != null)
            _director.stopped += OnDirectorStopped;
    }
    private void OnDisable()
    {
        _enterTrigger.OnPlayerEnteredRoom -= PlayerEnteredRoom;
        _passTrigger.OnPlayerPassedRoom -= PlayerPassedRoom;
        _checkPoint.Trigger.OnPlayerEnterCheckPoint -= OnPlayerEnterCheckPoint;

        if (_director != null)
            _director.stopped -= OnDirectorStopped;
    }
    private void OnPlayerEnterCheckPoint()
    {
        _enterTrigger.gameObject.SetActive(true);
        _barrier.FadeOutBarrier();
    }
    private void OnDirectorStopped(PlayableDirector director) => GameEvents.TriggerCinematicEnded();
    private void PlayerEnteredRoom() => PlayRoomIntro();
    private void PlayerPassedRoom() => DeactivateLasers();
    private void DeactivateLasers()
    {
        foreach (var laser in _lasersList)
            laser.PlayLaserDeactivateSequence();
    }
    private void PlayRoomIntro()
    {
        if (_hasPlayedIntro)
            return;

        _director.Play();
        _hasPlayedIntro = true;
    }
}
