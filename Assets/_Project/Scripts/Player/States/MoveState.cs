using UnityEngine;

public class MoveState : IState
{
    private readonly PlayerBase _player;

    public MoveState(PlayerBase player) => _player = player;

    private float _stopTimer;
    private const float STOP_THRESHOLD = 0.08f;
    private Vector3 _movementDirection;
    public void EnterState()
    {
        _player.AnimationController.PlayMoveAnimation(true);
        _player.AnimationController.PlaySprintAnimation(_player.IsSprint);
    }
    public void Tick()
    {
        Vector2 input = _player.Input.GetPlayerInput();
        Vector3 direction = new Vector3(input.x, 0f, input.y).normalized;

        if (direction.sqrMagnitude < 0.01f)
        {
            _stopTimer += Time.deltaTime;

            if (_stopTimer >= STOP_THRESHOLD)
            {
                _player.StateController.SwitchState(_player.IdleState);
            }
            return;
        }

        _stopTimer = 0f;

        _movementDirection = direction;
        HandleRotation();
    }
    public void FixedTick()
    {
        if (_stopTimer > 0f)
        {
            _player.Rb.linearVelocity = new Vector3(0f, _player.Rb.linearVelocity.y, 0f);
            return;
        }

        Vector3 targetVelocity = _movementDirection * _player.MovementSpeed;
        _player.Rb.linearVelocity = new Vector3(targetVelocity.x, _player.Rb.linearVelocity.y, targetVelocity.z);
    }
    public void ExitState()
    {
        _player.Rb.linearVelocity = new Vector3(0f, _player.Rb.linearVelocity.y, 0f);
        _player.AnimationController.PlayMoveAnimation(false);
        _player.AnimationController.PlaySprintAnimation(false);
    }
    private void HandleRotation()
    {
        if (_movementDirection == Vector3.zero) 
            return;

        Quaternion targetRotation = Quaternion.LookRotation(_movementDirection, Vector3.up);

        _player.Visual.transform.rotation = Quaternion.RotateTowards(
            _player.Visual.transform.rotation,
            targetRotation,
            _player.Data.RotationSpeed * Time.deltaTime
        );
    }
}
