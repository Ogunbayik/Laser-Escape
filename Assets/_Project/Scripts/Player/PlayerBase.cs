using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBase : MonoBehaviour
{
    [HideInInspector] public Rigidbody Rb;
    [HideInInspector] public PlayerInput Input;
    [HideInInspector] public PlayerMovement Movement;
    [HideInInspector] public PlayerInteract Interact;
    [HideInInspector] public PlayerAnimationController AnimationController;

    [Header("Data References")]
    public PlayerData Data;

    [Header("Transform References")]
    public Transform Visual;

    public PlayerStateController StateController {  get; private set; }

    public IdleState IdleState {  get; private set; }
    public MoveState MoveState { get; private set; }
    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        Input = GetComponent<PlayerInput>();
        Movement = GetComponent<PlayerMovement>();
        Interact = GetComponentInChildren<PlayerInteract>();
        AnimationController = GetComponentInChildren<PlayerAnimationController>();

        StateController = new PlayerStateController();
        IdleState = new IdleState(this);
        MoveState = new MoveState(this);
    }

    private void Start()
    {
        StateController.SwitchState(IdleState);
    }
    private void Update() => StateController.Tick();
    private void FixedUpdate() => StateController.FixedTick();
    public void Die()
    {
        Debug.Log("Player is die!");
        this.gameObject.SetActive(false);
    }
}
