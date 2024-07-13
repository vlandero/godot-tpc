using Godot;
using System;

public partial class PlayerIdleState : PlayerState
{
    protected override void EnterState()
    {
        playerNode.AnimPlayer.Play(GameConstants.STATE_IDLE);
        playerNode.ApplyIdle();
        playerNode.MoveAndSlide();
    }

    public override void _Input(InputEvent @event)
    {
        if(Input.IsActionJustPressed(GameConstants.INPUT_JUMP))
        {
            if(playerNode.IsOnFloor())
            {
                playerNode.StateMachineNode.SwitchState<PlayerJumpingState>();
            }
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if(playerNode.Direction != Vector3.Zero)
        {
            playerNode.StateMachineNode.SwitchState<PlayerWalkingState>();
        }

        playerNode.Move(delta);
    }
}
