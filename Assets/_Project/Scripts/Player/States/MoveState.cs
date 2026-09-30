using UnityEngine;

public class MoveState : IState
{
    private readonly PlayerBase _player;

    public MoveState(PlayerBase player) => _player = player;

    private Vector3 _movementDirection;
    public void EnterState()
    {
        _player.AnimationController.PlayMoveAnimation(true);
    }
    public void Tick()
    {
        Vector2 input = _player.Input.GetPlayerInput();
        Vector3 direction = new Vector3(input.x, 0f, input.y).normalized;

        if (direction.sqrMagnitude < 0.01f)
        {
            _player.StateController.SwitchState(_player.IdleState);
            return;
        }

        _movementDirection = direction * _player.Data.MovementSpeed;
        HandleRotation();
    }
    public void FixedTick()
    {
        _player.Rb.linearVelocity = new Vector3(_movementDirection.x, _player.Rb.linearVelocity.y, _movementDirection.z);
    }
    public void ExitState()
    {
        _player.Rb.linearVelocity = new Vector3(0f, _player.Rb.linearVelocity.y, 0f);
        _player.AnimationController.PlayMoveAnimation(false);
    }
    private void HandleRotation()
    {
        Quaternion targetRotation = Quaternion.LookRotation(_movementDirection, Vector3.up);

        _player.Visual.transform.rotation = Quaternion.Slerp(_player.Visual.transform.rotation, targetRotation, _player.Data.RotationSpeed * Time.deltaTime);
    }
}
