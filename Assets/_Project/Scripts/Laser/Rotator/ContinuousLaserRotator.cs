using UnityEngine;

public class ContinuousLaserRotator : LaserRotatorBase
{
    [Header("Rotation Settings")]
    [SerializeField] private float _rotationSpeed = 90f;

    private void Start() => IsRotating = true;
    protected override void HandleRotation()
    {
        var rotation = _rotationSpeed * Time.deltaTime;
        Head.transform.Rotate(Vector3.up * rotation);
    }

}
