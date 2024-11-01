using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbState : PlayerTouchingWallState
{
    public PlayerWallClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.SetVelocityY(playerData.wallClimbVelocity);

        if (!isExitingState)
        {
            if (yInput != 1 && !isExitingState)
            {
                stateMachine.ChangeState(player.WallGrabState);
            }
            
        }
    }

}
