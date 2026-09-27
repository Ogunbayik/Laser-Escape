using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public PlayerInput Input;
    public PlayerMovement Movement;
    private void Awake()
    {
        Input = GetComponent<PlayerInput>();
        Movement = GetComponent<PlayerMovement>();
    }
}
