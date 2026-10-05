using UnityEngine;

public abstract class LaserRotatorBase : MonoBehaviour
{
    [Header("Laser Type")]
    [SerializeField] public LaserType Type;

    [Header("Transform References")]
    [SerializeField] protected Transform Head;
    protected LaserBeam LaserBeam;
    public bool IsRotating;
    private void Awake() => LaserBeam = GetComponentInChildren<LaserBeam>();
    protected virtual void Update()
    {
        if(IsRotating)
        {
            HandleRotation();
        }
    }
    protected abstract void HandleRotation();
}

public enum LaserType
{
    Static,
    Continuous,
    StepRotatable,
    VariableSpeed
}