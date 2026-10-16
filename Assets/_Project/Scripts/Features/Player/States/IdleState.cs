using UnityEngine;

public class IdleState : IState
{
    private readonly PlayerBase _player;

    public IdleState(PlayerBase player) => _player = player;
    public void EnterState()
    {
        if (_player.Rb != null)
        {
            _player.Rb.linearVelocity = new Vector3(0f, _player.Rb.linearVelocity.y, 0f);
        }

        _player.AnimationController.PlayWalkAnimation(false);
    }
    public void Tick()
    {
        if (_player.IsMovementLocked) 
            return;

        _player.RegenerateStamina(Time.deltaTime);

        Vector2 input = _player.Input.GetPlayerInput();
        if(input != Vector2.zero)
        {
            if (_player.IsSprint && _player.CanSprint)
            {
                _player.StateController.SwitchState(_player.SprintState);
            }
            else
            {
                _player.StateController.SwitchState(_player.WalkState);
            }
        }
    }
    public void FixedTick() { }
    public void ExitState() { }

}
