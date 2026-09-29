using UnityEngine;

public class IdleState : IState
{
    private readonly PlayerBase _player;

    public IdleState(PlayerBase player) => _player = player;
    public void EnterState()
    {
        _player.AnimationController.PlayMoveAnimation(false);
    }
    public void Tick()
    {
        Vector2 input = _player.Input.GetPlayerInput();
        if(input != Vector2.zero)
        {
            _player.StateController.SwitchState(_player.MoveState);
        }
    }
    public void FixedTick() { }
    public void ExitState() { }

}
