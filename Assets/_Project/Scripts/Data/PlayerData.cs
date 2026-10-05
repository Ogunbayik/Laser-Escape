using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Scriptable Object/Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Movement Settings")]
    public float WalkSpeed;
    public float SprintSpeed;

    [Header("Rotation Settings")]
    public float RotationSpeed;

    [Header("Material References")]
    public Material DissolveMaterial;

    [Header("Duration Settings")]
    public float DissolveDuration;
    public float BurnDuration;

    [Header("Stamina Settings")]
    public float MaxStamina;
    public float StaminaDrainRate;
    public float StaminaRegenRate;
}
