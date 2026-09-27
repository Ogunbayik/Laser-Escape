using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Scriptable Object/Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Movement Settings")]
    public float MovementSpeed;

    [Header("Rotation Settings")]
    public float RotationSpeed;
}
