using UnityEngine;

public class StaticLaserRotator : LaserRotatorBase
{
    [Header("Identity Settings")]
    [SerializeField] private int _interactionID;
    [SerializeField] private bool _isActive;

    private void Start()
    {
        IsRotating = false;
        ToggleLaserBeam(_isActive);
    }
    protected override void HandleRotation() { }
    private void OnEnable() => InteractionEvents.OnButtonTriggered += ActivateLaserBeam;
    private void OnDisable() => InteractionEvents.OnButtonTriggered -= ActivateLaserBeam;
    private void ActivateLaserBeam(int buttonID, bool isActive)
    {
        if (_interactionID != buttonID)
            return;

        ToggleLaserBeam(isActive);
    }
    private void ToggleLaserBeam(bool isActive)
    {
        LaserBeam.enabled = isActive;
        LaserBeam.LineRenderer.enabled = isActive;
    }
}
