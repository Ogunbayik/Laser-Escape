using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public abstract class MovementBaseState : IState
{
    protected readonly PlayerBase Player;
    private Vector3 _movementDirection;
    private float _stopTimer;
    private const float STOP_THRESHOLD = 0.08f;

    protected MovementBaseState(PlayerBase player) => Player = player;

    public virtual void EnterState() { }

    public virtual void Tick()
    {
        if (Player.IsMovementLocked) 
            return;

        Vector2 input = Player.Input.GetPlayerInput();
        Vector3 direction = new Vector3(input.x, 0f, input.y).normalized;

        // Durma kontrolü
        if (direction.sqrMagnitude < 0.01f)
        {
            _stopTimer += Time.deltaTime;
            if (_stopTimer >= STOP_THRESHOLD)
            {
                Player.StateController.SwitchState(Player.IdleState);
            }
            return;
        }

        _stopTimer = 0f;
        _movementDirection = direction;
        HandleRotation();
    }

    public virtual void FixedTick()
    {
        if (_stopTimer > 0f)
        {
            Player.Rb.linearVelocity = new Vector3(0f, Player.Rb.linearVelocity.y, 0f);
            return;
        }

        Vector3 targetVelocity = _movementDirection * Player.MovementSpeed;
        Player.Rb.linearVelocity = new Vector3(targetVelocity.x, Player.Rb.linearVelocity.y, targetVelocity.z);
    }

    public virtual void ExitState()
    {
        Player.Rb.linearVelocity = new Vector3(0f, Player.Rb.linearVelocity.y, 0f);
    }

    private void HandleRotation()
    {
        if (_movementDirection == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(_movementDirection, Vector3.up);
        Player.Visual.transform.rotation = Quaternion.RotateTowards(
            Player.Visual.transform.rotation,
            targetRotation,
            Player.Data.RotationSpeed * Time.deltaTime
        );
    }
}
