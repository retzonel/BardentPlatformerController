using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            player.SetVelocityY(-playerData.wallSlideVeocity);

            if (grabInput && yInput == 0 && !isExitingState)
            {
                stateMachine.ChangeState(player.WallGrabState);
            }
            
        }
    }
}

