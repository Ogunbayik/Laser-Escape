using UnityEngine;

public class PassRoad : MonoBehaviour
{
    [Header("Identity Settings")]
    public int NextRoomID;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<PlayerBase>(out PlayerBase player))
        {
            //Player enter the pass Road
            CameraManager.Instance.SwitchCamera(CameraType.Pass);
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
}
