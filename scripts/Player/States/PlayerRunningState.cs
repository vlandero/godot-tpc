using Godot;
using System;

public partial class PlayerRunningState : PlayerState
{
    protected override void EnterState()
    {
        playerNode.AnimPlayer.Play(GameConstants.STATE_RUNNING);
        playerNode.currentSpeed = playerNode.RunSpeed;
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
        if(Input.IsActionJustReleased(GameConstants.INPUT_RUN))
        {
            playerNode.StateMachineNode.SwitchState<PlayerWalkingState>();
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

        playerNode.Move(delta);
    }
}
