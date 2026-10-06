using UnityEngine;

public abstract class BaseLaserController : MonoBehaviour
{
    protected LaserBeam _laserBeam;
    protected BaseLaserRotator _laserRotator;

    [Header("Activate Settings")]
    [SerializeField] protected bool IsActive;

    [Header("Visaul References")]
    [SerializeField] protected Transform _headVisual;

    public Transform Head => _headVisual;
    protected virtual void Awake()
    {
        if (_laserBeam == null)
            _laserBeam = GetComponentInChildren<LaserBeam>();

        if (_laserRotator == null)
            _laserRotator = GetComponent<BaseLaserRotator>();
    }
    protected virtual void Start() => ApplyLaserState(IsActive);
    protected void SetActiveState(bool isActive)
    {
        if (IsActive == isActive)
            return;

        IsActive = isActive;
        ApplyLaserState(isActive);
    }
    private void ApplyLaserState(bool isActive)
    {
        _laserBeam.enabled = isActive;

        if (_laserRotator != null)
            _laserRotator.enabled = isActive;
    }
}
