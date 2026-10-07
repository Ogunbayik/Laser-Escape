using Cinemachine;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [Header("Brain References")]
    [SerializeField] private CinemachineBrain _brain;


    [Header("Camera References")]
    public CinemachineVirtualCamera _gamePlayerCam;
    public CinemachineVirtualCamera _spawnCam;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        SwitchCamera(CameraType.Gameplay);
    }
    public async UniTask MoveCameraToRespawnPoint()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: this.GetCancellationTokenOnDestroy());

        SwitchCamera(CameraType.Spawn);

        await UniTask.Yield(PlayerLoopTiming.Update, this.GetCancellationTokenOnDestroy());

        await UniTask.WaitWhile(() => _brain.IsBlending, cancellationToken: this.GetCancellationTokenOnDestroy());

        SwitchCamera(CameraType.Gameplay);
        Debug.Log("Current Camera is Gameplay");
    }
    private void SwitchCamera(CameraType type)
    {
        _gamePlayerCam.Priority = 1;
        _spawnCam.Priority = 1;

        switch(type)
        {
            case CameraType.Gameplay: _gamePlayerCam.Priority = 10;
                break;
            case CameraType.Spawn: _spawnCam.Priority = 10;
                break;
        }
    }
}


public enum CameraType
{
    Gameplay,
    Spawn
}
