using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public static CheckPointManager Instance;

    public CheckPoint CurrentCheckPoint;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public void SetCheckPoint(CheckPoint nextCheckPoint)
    {
        if (nextCheckPoint == null)
            return;

        if (CurrentCheckPoint == nextCheckPoint)
            return;

        CurrentCheckPoint = nextCheckPoint;

        CameraManager.Instance.FocusSpawnCameraOnCheckpoint(CurrentCheckPoint);
        CameraManager.Instance.SetSpawnCameraOffset(CurrentCheckPoint.OffsetSpawnCam);
    }
}
