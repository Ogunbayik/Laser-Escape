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
    private void Update() => HandleCountdownTimer();
    private void HandleCountdownTimer()
    {
        _currentTime -= Time.deltaTime;

        if(_currentTime <= 0)
        {
            SetActiveState(!IsActive);
            SetTimer();
        }
    }
    private void SetTimer() => _currentTime = IsActive ? _activeDuration : _deactiveDuration;
}
