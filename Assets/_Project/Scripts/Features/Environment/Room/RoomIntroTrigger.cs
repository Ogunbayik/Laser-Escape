using System;
using UnityEngine;

public class RoomIntroTrigger : MonoBehaviour
{
    public event Action OnPlayerTriggeredIntro;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerBase>(out PlayerBase player))
        {
            OnPlayerTriggeredIntro?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
