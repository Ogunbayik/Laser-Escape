using UnityEngine;

public abstract class BaseLaserRotator : MonoBehaviour
{
    protected BaseLaserController _laser;
    protected LaserRotationType _rotationType;
    protected LaserBeam _laserBeam;

    [HideInInspector] public bool IsRotating;
    public LaserRotationType RotationType => _rotationType;

    protected virtual void Awake()
    {
        _laser = GetComponent<BaseLaserController>();
        _laserBeam = GetComponentInChildren<LaserBeam>();
    }
    protected virtual void Start() { }
    protected virtual void Update()
    {
        if(IsRotating)
        {
            HandleRotation();
        }
    }
    protected abstract void HandleRotation();
}

public enum LaserRotationType
{
    Continuous,
    StepRotatable,
    VariableSpeed
}