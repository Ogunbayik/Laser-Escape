using System;
using UnityEngine;

public class RoomPassTrigger : MonoBehaviour
{
    public event Action OnPlayerPassedRoom;

    private bool _hasPassed = false;
    private void OnTriggerEnter(Collider other)
    {
        if (_hasPassed)
            return;

        if (other.gameObject.TryGetComponent<PlayerBase>(out PlayerBase player))
        {
            _hasPassed = true;
            OnPlayerPassedRoom?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
