using UnityEngine;

public class PassRoad : MonoBehaviour
{
    [Header("Right Camera Settings")]
    [SerializeField] private Vector3 _offsetPassCamRight;
    [SerializeField] private Vector3 _aimPassCamRight;


    [Header("Left Camera Settings")]
    [SerializeField] private Vector3 _offsetPassCamLeft;
    [SerializeField] private Vector3 _aimPassCamLeft;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<PlayerBase>(out PlayerBase player))
        {
            Vector3 incomingDir = player.transform.position - transform.position;
            float sideCheck = Vector3.Dot(transform.right, incomingDir);

            if (sideCheck > 0)
            {
                ApplyPassCameraSettings(_offsetPassCamRight, _aimPassCamRight);
            }
            else
            {
                ApplyPassCameraSettings(_offsetPassCamLeft, _aimPassCamLeft);
            }
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
    private void ApplyPassCameraSettings(Vector3 offset, Vector3 aim)
    {
        CameraManager.Instance.SetPassCameraOffsetAndAim(offset, aim);
        CameraManager.Instance.SwitchCamera(CameraType.Pass);
    }
}
