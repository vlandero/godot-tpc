using Godot;
using System;

public partial class PlayerGetUpState : PlayerState
{
    protected override void EnterState()
    {
        playerNode.AnimPlayer.AnimationFinished += OnAnimationFinished;
        playerNode.AnimPlayer.Play("GameConstants.STATE_GETUP");
    }

    protected override void ExitState()
    {
        playerNode.AnimPlayer.AnimationFinished -= OnAnimationFinished;
    }

    private void OnAnimationFinished(StringName animName)
    {
        if(animName == "GameConstants.STATE_GETUP")
        {
            playerNode.StateMachineNode.SwitchState<PlayerIdleState>();
        }
    }
}
