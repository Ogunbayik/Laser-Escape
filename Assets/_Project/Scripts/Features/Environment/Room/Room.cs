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
    [SerializeField] private RoomIntroTrigger _introTrigger;
    [SerializeField] private RoomPassTrigger _passTrigger;


    [Header("Barrier References")]
    [SerializeField] private Barrier _enterBarrier;
    [SerializeField] private Barrier _exitBarrier;


    private bool _hasPlayedIntro = false;
    public RoomPassTrigger PassTrigger => _passTrigger;
    private void Awake()
    {
        _director = GetComponent<PlayableDirector>();
    }
    public void Initialize()
    {
        _introTrigger.gameObject.SetActive(false);
        _enterBarrier.gameObject.SetActive(true);
        _exitBarrier.gameObject.SetActive(false);

        if (_checkPoint != null && _checkPoint.Trigger != null)
            _checkPoint.Trigger.OnPlayerEnterCheckPoint += OnPlayerEnterCheckPoint;
    }
    private void OnEnable()
    {
        _introTrigger.OnPlayerTriggeredIntro += PlayRoomIntro;
        _passTrigger.OnPlayerPassedRoom += OnPassedRoom;

        if(_director != null)
            _director.stopped += OnDirectorStopped;
    }
    private void OnDisable()
    {
        _checkPoint.Trigger.OnPlayerEnterCheckPoint -= OnPlayerEnterCheckPoint;
        _introTrigger.OnPlayerTriggeredIntro -= PlayRoomIntro;
        _passTrigger.OnPlayerPassedRoom -= OnPassedRoom;

        if (_director != null)
            _director.stopped -= OnDirectorStopped;
    }
    private void OnPlayerEnterCheckPoint()
    {
        _introTrigger.gameObject.SetActive(true);
        _enterBarrier.FadeOutBarrier();
    }
    private void OnDirectorStopped(PlayableDirector director) => GameEvents.TriggerCinematicEnded();
    private void OnPassedRoom()
    {
        foreach (var laser in _lasersList)
            laser.PlayLaserDeactivateSequence();

        _exitBarrier.FadeInBarrier();
    }
    private void PlayRoomIntro()
    {
        if (_hasPlayedIntro)
            return;

        GameEvents.TriggerCinematicStarted();
        _director.Play();
        _hasPlayedIntro = true;
    }
}
