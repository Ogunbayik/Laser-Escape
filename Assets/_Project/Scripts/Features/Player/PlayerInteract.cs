using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private PlayerBase _player;

    private StepLaserRotator _stepLaser;
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
        if (_stepLaser == null)
            return;

        if (_stepLaser.RotationType != LaserRotationType.StepRotatable)
            return;

        _stepLaser.RotateToNextStep();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<StepLaserRotator>(out StepLaserRotator laser))
        {
            _stepLaser = laser;
            _player.InteractView.ShowInteractView();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.TryGetComponent<StepLaserRotator>(out StepLaserRotator laser))
        {
            if (_stepLaser = laser)
            {
                _stepLaser = null;
                _player.InteractView.HideInteractView();
            }
        }
    }
}
