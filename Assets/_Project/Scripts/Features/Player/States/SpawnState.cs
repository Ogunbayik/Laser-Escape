using UnityEngine;

public class SpawnState : IState
{
    private readonly PlayerBase _player;


    public SpawnState(PlayerBase player) => _player = player;

    public void EnterState()
    {
        _player.AnimationController.PlayStandUpAnimation();

        _player.AnimationController.OnPlayerSpawned += OnPlayerSpawned;
    }
    public void ExitState()
    {
        _player.SetDeadStatus(false);

        _player.AnimationController.OnPlayerSpawned -= OnPlayerSpawned;
    }

    public void FixedTick()
    {

    }

    public void Tick()
    {

    }
    private void OnPlayerSpawned() => _player.ReturnToIdle();

}
