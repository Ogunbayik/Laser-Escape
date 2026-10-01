using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBase : MonoBehaviour
{
    [HideInInspector] public Rigidbody Rb;
    [HideInInspector] public PlayerInput Input;
    [HideInInspector] public PlayerInteract Interact;
    [HideInInspector] public PlayerAnimationController AnimationController;

    [Header("Data References")]
    public PlayerData Data;

    [Header("Transform References")]
    public Transform Visual;

    public PlayerStateController StateController {  get; private set; }

    public IdleState IdleState {  get; private set; }
    public MoveState MoveState { get; private set; }
    public DeadState DeadState { get; private set; }

    [HideInInspector] public Material InstancedMaterial;
    [HideInInspector] public float MovementSpeed;

    public bool IsSprint;
    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        Input = GetComponent<PlayerInput>();
        Interact = GetComponentInChildren<PlayerInteract>();
        AnimationController = GetComponentInChildren<PlayerAnimationController>();

        StateController = new PlayerStateController();
        IdleState = new IdleState(this);
        MoveState = new MoveState(this);
        DeadState = new DeadState(this);
    }
    private void Start()
    {
        Initialize();
    }
    private void OnEnable()
    {
        if (Input != null)
            Input.OnSprintStateChanged += OnSprintStateChanged;
    }
    private void OnDisable()
    {
        if (Input != null)
            Input.OnSprintStateChanged -= OnSprintStateChanged;
    }
    private void OnSprintStateChanged(bool isActive)
    {
        IsSprint = isActive;
        MovementSpeed = isActive ? Data.SprintSpeed : Data.WalkSpeed;

        if (StateController.CurrentState == MoveState)
        {
            AnimationController.PlaySprintAnimation(isActive);
        }
    }
    private void Initialize()
    {
        StateController.SwitchState(IdleState);
        IsSprint = false;
        MovementSpeed = Data.WalkSpeed;

        InitializeMaterial();
    }
    private void InitializeMaterial()
    {
        if (Data.DissolveMaterial == null)
            Debug.LogError("Dissolve Material atanmalı..");

        InstancedMaterial = new Material(Data.DissolveMaterial);
        SkinnedMeshRenderer[] allRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (var renderer in allRenderers)
        {
            if (renderer.gameObject.name == "head_eyes_low")
                continue;

            renderer.material = InstancedMaterial;
        }

        InstancedMaterial.SetFloat(GameConstant.ShaderProperties.DissolveID, GameConstant.ShaderValue.MaxValue);
    }
    private void Update() => StateController.Tick();
    private void FixedUpdate() => StateController.FixedTick();
    public void Die() => StateController.SwitchState(DeadState);
}
