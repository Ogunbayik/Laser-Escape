using System;
using UnityEngine;

public class CheckPointTrigger : MonoBehaviour
{
    public event Action OnPlayerEnterCheckPoint;

    public bool HasTriggered;
    private void OnTriggerEnter(Collider other)
    {
        if (HasTriggered)
            return;
        
        if (other.gameObject.TryGetComponent<PlayerBase>(out PlayerBase player))
        {
            HasTriggered = true;
            OnPlayerEnterCheckPoint?.Invoke();
        }
    }
}
