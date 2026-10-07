using UnityEngine;

public class PassRoad : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<PlayerBase>(out PlayerBase player))
        {
            //Player enter the pass Road
            CameraManager.Instance.SwitchCamera(CameraType.Pass);
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
}
