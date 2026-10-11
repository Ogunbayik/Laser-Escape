using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public static CheckPointManager Instance;

    [Header("Check Point References")]
    [SerializeField] private CheckPoint _initialCheckPoint;
    
    public CheckPoint CurrentCheckPoint;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Start() => Initialize();
    private void Initialize()
    {
        SetCheckPoint(_initialCheckPoint);
        CameraManager.Instance.SetSpawnCameraOffset(_initialCheckPoint.OffsetSpawnCam);

        _initialCheckPoint.PlantGroup.ActivatePlants();
    }
    public void SetCheckPoint(CheckPoint nextCheckPoint)
    {
        if (nextCheckPoint == null)
            return;

        if (CurrentCheckPoint == nextCheckPoint)
            return;

        if (CurrentCheckPoint != null && CurrentCheckPoint.CheckPointID >= nextCheckPoint.CheckPointID)
            return;

        CurrentCheckPoint = nextCheckPoint;

        CameraManager.Instance.FocusSpawnCameraOnCheckpoint(CurrentCheckPoint);
        CameraManager.Instance.SetSpawnCameraOffset(CurrentCheckPoint.OffsetSpawnCam);
    }
}
