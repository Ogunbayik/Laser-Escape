using UnityEngine;
using OB.Utils;

public class VariableSpeedLaserRotator : BaseLaserRotator
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

    protected override void Start()
    {
        base.Start();

        _rotationType = LaserRotationType.VariableSpeed;
        _isFirstSpeedActive = true;
        _currentRotationSpeed = _firstSpeed;
        IsRotating = true;
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
        _laser.Head.transform.Rotate(Vector3.up * rotation);
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
