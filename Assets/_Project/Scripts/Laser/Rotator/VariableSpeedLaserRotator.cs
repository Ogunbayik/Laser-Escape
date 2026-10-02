using UnityEngine;
using OB.Utils;

public class VariableSpeedLaserRotator : LaserRotatorBase
{
    [Header("Rotation Settings")]
    [SerializeField] private float _firstSpeed;
    [SerializeField] private float _secondSpeed;

    [Header("Interval Settings")]
    [SerializeField] private float _minChangeInterval;
    [SerializeField] private float _maxChangeInterval;

    private float _currentIntervalTime;
    private float _currentRotationSpeed;

    private bool _isFirstSpeedActive;

    private void Start()
    {
        IsRotating = true;
        _isFirstSpeedActive = true;
        _currentRotationSpeed = _firstSpeed;

        ResetIntervalTime();
    }
    protected override void Update()
    {
        if (!IsRotating) 
            return;

        HandleCountdownInterval();

        base.Update();
    }
    protected override void HandleRotation()
    {
        var rotation = _currentRotationSpeed * Time.deltaTime;
        Head.transform.Rotate(Vector3.up * rotation);
    }
    private void HandleCountdownInterval()
    {
        _currentIntervalTime -= Time.deltaTime;

        if (_currentIntervalTime <= 0)
        {
            SetRotationSpeed();
            ResetIntervalTime();
        }
    }
    private void SetRotationSpeed()
    {
        _isFirstSpeedActive = !_isFirstSpeedActive;
        _currentRotationSpeed = _isFirstSpeedActive ? _firstSpeed : _secondSpeed;
    }
    private void ResetIntervalTime() => _currentIntervalTime = RandomUtils.GetRandomFloat(_minChangeInterval, _maxChangeInterval);
}
