using UnityEngine;

public class WalkState : MovementBaseState
{
    public WalkState(PlayerBase player) : base(player) { }

    public override void EnterState()
    {
        base.EnterState();
        Player.MovementSpeed = Player.Data.WalkSpeed;
        Player.AnimationController.PlayWalkAnimation(true);
        Player.AnimationController.PlaySprintAnimation(false);
    }

    public override void Tick()
    {
        base.Tick();
        // Yürürken stamina dolsun
        Player.RegenerateStamina(Time.deltaTime);

        // Sprint tuşuna basılı tutuyorsa VE staminası varsa Sprint'e geç
        if (Player.IsSprint && Player.CanSprint)
        {
            Player.StateController.SwitchState(Player.SprintState);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
        Player.AnimationController.PlayWalkAnimation(false);
    }
}
