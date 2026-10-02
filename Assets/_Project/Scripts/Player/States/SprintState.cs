using UnityEngine;

public class SprintState : MovementBaseState
{
   public SprintState(PlayerBase player) : base(player) { }

    public override void EnterState()
    {
        base.EnterState();
        Player.MovementSpeed = Player.Data.SprintSpeed;
        Player.AnimationController.PlayWalkAnimation(true);
        Player.AnimationController.PlaySprintAnimation(true);
    }

    public override void Tick()
    {
        base.Tick();

        // Stamina harca
        Player.DrainStamina(Time.deltaTime);

        // Tuş bırakıldıysa VEYA stamina bittiyse Walk'a dön
        if (!Player.IsSprint || !Player.CanSprint)
        {
            Player.StateController.SwitchState(Player.WalkState);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
        Player.AnimationController.PlayWalkAnimation(false);
        Player.AnimationController.PlaySprintAnimation(false);
    }
}
