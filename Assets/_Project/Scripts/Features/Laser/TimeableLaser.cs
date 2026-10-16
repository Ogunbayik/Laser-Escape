using UnityEngine;

public class TimeableLaser : BaseLaserController
{
    [Header("Duration Settings")]
    [SerializeField] private float _activeDuration;
    [SerializeField] private float _deactiveDuration;

    private float _currentTime;
    protected override void Start()
    {
        base.Start();
        SetTimer();
    }
    private void Update()
    {
        if (_currentStatus == LaserStatus.Active)
            HandleCountdownTimer();
    }
    private void HandleCountdownTimer()
    {
        _currentTime -= Time.deltaTime;

        if(_currentTime <= 0)
        {
            SetLaserBeamActivateStatus(!IsLaserBeamActive);
            SetTimer();
        }
    }
    private void SetTimer() => _currentTime = IsLaserBeamActive ? _activeDuration : _deactiveDuration;
}
