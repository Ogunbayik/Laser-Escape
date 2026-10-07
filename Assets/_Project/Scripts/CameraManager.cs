using Cinemachine;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [Header("Brain References")]
    [SerializeField] private CinemachineBrain _brain;


    [Header("General Camera References")]
    public CinemachineVirtualCamera _gamePlayerCam;
    public CinemachineVirtualCamera _spawnCam;

    [Header("Player Camera References")]
    public CinemachineVirtualCamera _passCam;
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
    }
    public void SwitchCamera(CameraType type)
    {
        _gamePlayerCam.Priority = GameConstant.CameraPriority.DEACTIVE_PRIORITY;
        _spawnCam.Priority = GameConstant.CameraPriority.DEACTIVE_PRIORITY;
        _passCam.Priority = GameConstant.CameraPriority.DEACTIVE_PRIORITY;

        switch(type)
        {
            case CameraType.Gameplay: _gamePlayerCam.Priority = GameConstant.CameraPriority.ACTIVE_PRIORITY;
                break;
            case CameraType.Spawn: _spawnCam.Priority = GameConstant.CameraPriority.ACTIVE_PRIORITY;
                break;
            case CameraType.Pass: _passCam.Priority = GameConstant.CameraPriority.ACTIVE_PRIORITY;
                break;
        }
    }
}


public enum CameraType
{
    Gameplay,
    Spawn,
    Pass
}
