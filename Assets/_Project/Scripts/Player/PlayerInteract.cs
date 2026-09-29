using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private PlayerBase _player;

    private LaserBeam _targetLaserBeam;
    private void Awake()
    {
        _player = GetComponentInParent<PlayerBase>();
    }

    private void Start()
    {
        _player.Input.OnPressedInteract += Input_OnPressedInteract;
    }

    private void Input_OnPressedInteract()
    {
        if (_targetLaserBeam == null)
            return;

        _targetLaserBeam.RotateLaser();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<LaserBeam>(out LaserBeam laserBeam))
            _targetLaserBeam = laserBeam;
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.TryGetComponent<LaserBeam>(out LaserBeam laserBeam))
        {
            if (_targetLaserBeam == laserBeam)
                _targetLaserBeam = null;
        }
    }
}
