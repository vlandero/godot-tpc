using Godot;
using System;

public partial class PlayerWalkingState : PlayerState
{
    protected override void EnterState()
    {
        playerNode.AnimPlayer.Play(GameConstants.STATE_WALKING);
        playerNode.currentSpeed = playerNode.WalkSpeed;
    }

    public override void _Input(InputEvent @event)
    {
        if(Input.IsActionJustPressed(GameConstants.INPUT_JUMP))
        {
            if(playerNode.IsOnFloor())
            {
                playerNode.StateMachineNode.SwitchState<PlayerJumpingState>();
                return;
            }
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        playerNode.ApplyDirection(delta);
        if(playerNode.Direction == Vector3.Zero)
        {
            playerNode.StateMachineNode.SwitchState<PlayerIdleState>();
            return;
        }
        if(Input.IsActionPressed(GameConstants.INPUT_RUN))
        {
            playerNode.StateMachineNode.SwitchState<PlayerRunningState>();
        }

        playerNode.Move(delta);
    }
}
