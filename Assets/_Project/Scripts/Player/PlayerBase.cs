using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [HideInInspector] public PlayerInput Input;
    [HideInInspector] public PlayerMovement Movement;
    [HideInInspector] public PlayerInteract Interact;

    [Header("Data References")]
    public PlayerData Data;
    private void Awake()
    {
        Input = GetComponent<PlayerInput>();
        Movement = GetComponent<PlayerMovement>();
        Interact = GetComponentInChildren<PlayerInteract>();
    }
}
