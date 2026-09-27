using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Scriptable Object/Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Movement Data")]
    public float MovementSpeed;
}
