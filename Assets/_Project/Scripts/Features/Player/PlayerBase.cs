using DG.Tweening;
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBase : MonoBehaviour, ILaserTargetable
{
    public event Action<float, float> OnStaminaChanged;
    public event Action OnPlayerDied;

    [HideInInspector] public Rigidbody Rb;
    [HideInInspector] public PlayerInput Input;
    [HideInInspector] public PlayerInteract Interact;
    [HideInInspector] public PlayerInteractView InteractView;
    [HideInInspector] public PlayerAnimationController AnimationController;

    [Header("Data References")]
    public PlayerData Data;

    [Header("Transform References")]
    public Transform Visual;

    public PlayerStateController StateController {  get; private set; }

    public IdleState IdleState {  get; private set; }
    public DeadState DeadState { get; private set; }
    public WalkState WalkState { get; private set; }
    public SprintState SprintState { get; private set; }

    [HideInInspector] public Material InstancedMaterial;
    [HideInInspector] public float MovementSpeed;


    public float CurrentStamina { get; private set; }
    public bool IsExhausted { get; private set; }
    public bool IsSprint;
    public bool CanSprint => CurrentStamina > 0f && !IsExhausted;

    private bool _isDead;
    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        Input = GetComponent<PlayerInput>();
        Interact = GetComponentInChildren<PlayerInteract>();
        InteractView = GetComponentInChildren<PlayerInteractView>();
        AnimationController = GetComponentInChildren<PlayerAnimationController>();

        StateController = new PlayerStateController();
        IdleState = new IdleState(this);
        DeadState = new DeadState(this);
        WalkState = new WalkState(this);
        SprintState = new SprintState(this);
    }
    private void Start()
    {
        Initialize();
    }
    private void OnEnable()
    {
        if (Input != null)
            Input.OnSprintStateChanged += OnSprintStateChanged;

        AnimationController.OnPlayerSpawned += ReturnToIdle;
    }
    private void OnDisable()
    {
        if (Input != null)
            Input.OnSprintStateChanged -= OnSprintStateChanged;

        AnimationController.OnPlayerSpawned -= ReturnToIdle;
    }
    private void OnSprintStateChanged(bool isActive)
    {
        IsSprint = isActive;
    }
    private void Initialize()
    {
        StateController.SwitchState(IdleState);
        IsSprint = false;
        MovementSpeed = Data.WalkSpeed;
        CurrentStamina = Data.MaxStamina;

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

        InstancedMaterial.SetFloat(GameConstant.ShaderProperties.DissolveID, GameConstant.MathConstant.NORMALIZED_MAX);
    }
    private void Update() => StateController.Tick();
    private void FixedUpdate() => StateController.FixedTick();
    public void HitByLaser(Vector3 hitPoint, Vector3 hitNormal)
    {
        if (_isDead)
            return;

        SetDeadStatus(true);

        VFXManager.Instance?.PlayOneShotImpact(hitPoint, hitNormal);

        OnPlayerDied?.Invoke();
        StateController.SwitchState(DeadState);
    }
    public void LaserLost() { }

    public void DrainStamina(float deltaTime)
    {
        float prev = CurrentStamina;
        CurrentStamina = Mathf.Max(0f, CurrentStamina - (Data.StaminaDrainRate * deltaTime));

        if (CurrentStamina <= 0f) 
            IsExhausted = true;

        if (!Mathf.Approximately(prev, CurrentStamina))
        {
            OnStaminaChanged?.Invoke(CurrentStamina, Data.MaxStamina);
        }   
    }
    public void RegenerateStamina(float deltaTime)
    {
        if (CurrentStamina >= Data.MaxStamina) 
            return;

        float prev = CurrentStamina;
        CurrentStamina = Mathf.Min(Data.MaxStamina, CurrentStamina + (Data.StaminaRegenRate * deltaTime));

        if (IsExhausted && CurrentStamina >= Data.MaxStamina * 0.5f)
        {
            IsExhausted = false;
        }

        if (!Mathf.Approximately(prev, CurrentStamina))
        {
            OnStaminaChanged?.Invoke(CurrentStamina, Data.MaxStamina);
        }
    }
    public void ResetStamina() => CurrentStamina = Data.MaxStamina;
    public void SetDeadStatus(bool isDead) => _isDead = isDead;
    public void ReturnToIdle() => StateController.SwitchState(IdleState);
}
