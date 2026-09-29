using UnityEngine;

public interface IState
{
    void EnterState();
    void ExitState();
    void Tick();
    void FixedTick();
}
