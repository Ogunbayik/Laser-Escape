using UnityEngine;

public class ContinuousLaserRotator : BaseLaserRotator
{
    [Header("Rotation Settings")]
    [SerializeField] private float _rotationSpeed;

    protected override void Start()
    {
        _rotationType = LaserRotationType.Continuous;
        IsRotating = true;
    }
    protected override void HandleRotation()
    {
        var rotation = _rotationSpeed * Time.deltaTime;
        _laser.Head.transform.Rotate(Vector3.up * rotation);
    }

}
