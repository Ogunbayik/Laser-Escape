using UnityEngine;

public class TimeableLaser : BaseLaserController
{
    [Header("Duration Settings")]
    [SerializeField] private float _toggleDuration;
    private float _currentTime;

    protected override void Start()
    {
        base.Start();
        ResetTimer();
    }
    private void Update() => HandleCountdownTimer();
    private void HandleCountdownTimer()
    {
        _currentTime -= Time.deltaTime;

        if(_currentTime <= 0)
        {
            SetActiveState(!IsActive);
            ResetTimer();
        }
    }
    private void ResetTimer() => _currentTime = _toggleDuration;
}
