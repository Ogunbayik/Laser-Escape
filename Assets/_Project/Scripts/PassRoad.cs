using UnityEngine;

public class PassRoad : MonoBehaviour
{
    [Header("Identity Settings")]
    public int NextRoomID;

    [Header("Pass Camera Settings")]
    [SerializeField] private Vector3 OffsetPassCam;
    [SerializeField] private Vector3 OffsetAimPassCam;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<PlayerBase>(out PlayerBase player))
        {
            //Player enter the pass Road
            ApplyPassCameraSettings();
            GameEvents.TriggerRoomTransition(NextRoomID);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.TryGetComponent<PlayerBase>(out PlayerBase player))
        {
            //Player exit the pass Road
            CameraManager.Instance.SwitchCamera(CameraType.Gameplay);
        }
    }

    private void ApplyPassCameraSettings()
    {
        CameraManager.Instance.SetPassCameraOffsetAndAim(OffsetPassCam, OffsetAimPassCam);
        CameraManager.Instance.SwitchCamera(CameraType.Pass);
    }
}
