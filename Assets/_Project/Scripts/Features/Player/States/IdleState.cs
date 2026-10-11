using UnityEngine;

public class IdleState : IState
{
    private readonly PlayerBase _player;

    public IdleState(PlayerBase player) => _player = player;
    public void EnterState()
    {
        _player.AnimationController.PlayWalkAnimation(false);
    }
    public void Tick()
    {
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
