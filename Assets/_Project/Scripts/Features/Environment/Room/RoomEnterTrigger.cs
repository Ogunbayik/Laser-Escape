using System;
using UnityEngine;

public class RoomEnterTrigger : MonoBehaviour
{
    private bool _hasEntered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (_hasEntered)
            return;

        if(other.gameObject.TryGetComponent<PlayerBase>(out PlayerBase player))
        {
            _hasEntered = true;
            player.AuraView.ToggleAura(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerBase>(out PlayerBase player))
        {
            _hasEntered = false;
            player.AuraView.ToggleAura(false);
        }
    }
}
