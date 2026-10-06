using UnityEngine;

public class StepLaserRotator : BaseLaserRotator
{
    [Header("Rotation Settings")]
    [SerializeField] private float _stepAngle;
    [SerializeField] private float _rotateSpeed;

    private Quaternion _targetQuaternion;
    protected override void Start()
    {
        IsRotating = false;
        _rotationType = LaserRotationType.StepRotatable;
        _targetQuaternion = _laser.Head.transform.localRotation;
    }
    public void RotateToNextStep()
    {
        if (IsRotating) 
            return;

        _targetQuaternion *= Quaternion.Euler(0f, _stepAngle, 0f);
        IsRotating = true;
    }
    protected override void HandleRotation()
    {
        _laser.Head.transform.localRotation = Quaternion.RotateTowards(
            _laser.Head.transform.localRotation,
            _targetQuaternion,
            _rotateSpeed * Time.deltaTime
        );

        if (Quaternion.Angle(_laser.Head.transform.localRotation, _targetQuaternion) < 0.01f)
        {
            _laser.Head.transform.localRotation = _targetQuaternion;
            IsRotating = false;
        }
    }
}
