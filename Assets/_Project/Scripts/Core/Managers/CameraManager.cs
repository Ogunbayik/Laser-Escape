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

    private CinemachineTransposer _spawnTransposer;
    private CinemachineTransposer _passTransposer;
    private CinemachineComposer _passComposer;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        _spawnTransposer = _spawnCam.GetCinemachineComponent<CinemachineTransposer>();
        _passTransposer = _passCam.GetCinemachineComponent<CinemachineTransposer>();
        _passComposer = _passCam.GetCinemachineComponent<CinemachineComposer>();
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
    public void SetSpawnCameraOffset(Vector3 offset) => _spawnTransposer.m_FollowOffset = offset;
    public void FocusSpawnCameraOnCheckpoint(CheckPoint nextCheckPoint)
    {
        _spawnCam.m_Follow = nextCheckPoint.SpawnPosition;
        _spawnCam.m_LookAt = nextCheckPoint.SpawnPosition;
    }
    public void SetPassCameraOffsetAndAim(Vector3 offset, Vector3 aimOffset)
    {
        if (_passTransposer != null)
            _passTransposer.m_FollowOffset = offset;

        if (_passComposer != null)
            _passComposer.m_TrackedObjectOffset = aimOffset;
    }
}


public enum CameraType
{
    Gameplay,
    Spawn,
    Pass
}
