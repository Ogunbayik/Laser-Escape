using UnityEngine;

public abstract class LaserRotatorBase : MonoBehaviour
{
    [Header("Laser Type")]
    [SerializeField] public LaserType Type;

    [Header("Transform References")]
    [SerializeField] protected Transform Head;

    public bool IsRotating;

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