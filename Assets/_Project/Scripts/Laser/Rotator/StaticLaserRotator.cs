using UnityEngine;

public class StaticLaserRotator : LaserRotatorBase
{
    private void Start() => IsRotating = false;
    protected override void HandleRotation() { }

}
