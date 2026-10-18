using UnityEngine;

public class PlayerStateController
{
    public IState CurrentState { get; private set; }

    public void Initialize(IState startingState)
    {
        CurrentState = startingState;
        CurrentState.EnterState();
    }
    public void SwitchState(IState newState)
    {
        if (CurrentState == newState)
            return;

        CurrentState?.ExitState();
        CurrentState = newState;
        CurrentState.EnterState();
    }
    public void Tick() => CurrentState?.Tick();
    public void FixedTick() => CurrentState?.FixedTick();
}
