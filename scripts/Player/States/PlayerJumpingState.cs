using Godot;
using System;

public partial class PlayerJumpingState : PlayerState
{
    protected override void EnterState()
    {
        playerNode.AnimPlayer.Play(GameConstants.STATE_IDLE);
        playerNode.Jump();
        playerNode.MoveAndSlide();
    }

    public override void _PhysicsProcess(double delta)
    {
        if(playerNode.IsOnFloor())
        {
            playerNode.StateMachineNode.SwitchState<PlayerIdleState>();
        }
        playerNode.ApplyDirection(delta); // TODO apply a lower speed while jumping
        playerNode.Move(delta);
    }
}
