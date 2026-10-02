using UnityEngine;

public class StepLaserRotator : LaserRotatorBase
{
    [Header("Rotation Settings")]
    [SerializeField] private float _stepAngle = 90f;
    [SerializeField] private float _rotateSpeed = 360f;

    private Quaternion _targetQuaternion;

    private void Start()
    {
        IsRotating = false;
        _targetQuaternion = Head.transform.localRotation;
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
        Head.transform.localRotation = Quaternion.RotateTowards(
            Head.transform.localRotation,
            _targetQuaternion,
            _rotateSpeed * Time.deltaTime
        );

        if (Quaternion.Angle(Head.transform.localRotation, _targetQuaternion) < 0.01f)
        {
            Head.transform.localRotation = _targetQuaternion;
            IsRotating = false;
        }
    }
}
