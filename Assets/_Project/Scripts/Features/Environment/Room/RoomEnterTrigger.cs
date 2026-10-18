using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class RoomEnterTrigger : MonoBehaviour
{
    public event Action OnPlayerEnteredRoom;

    private bool _hasEntered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (_hasEntered)
            return;

        if(other.gameObject.TryGetComponent<PlayerBase>(out PlayerBase player))
        {
            _hasEntered = true;
            OnPlayerEnteredRoom?.Invoke();
            gameObject.SetActive(false);
            GameEvents.TriggerCinematicStarted();
        }
    }
}
