using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int wallJumpDir;

    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.InputHandler.UseJumpInput();
        // player.JumpState.ResetAmountOfJumps();
        player.SetVelocity(playerData.wallJumpVelocity, playerData.wallJumpAngle, wallJumpDir);
        player.CheckIfShouldFlip(wallJumpDir);
        player.JumpState.DecreaseAmountOfJumpsLeft();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
        player.Anim.SetFloat("xVelocity", Mathf.Abs(player.CurrentVelocity.x));

        if (Time.time >= (startTime + playerData.wallJumpTime))
        {
            isAbilityDone = true;
        }
    }

    public void DetermineWallJumpDir(bool isToucingWall)
    {
        if (isToucingWall)
        {
            wallJumpDir = -player.FaceingDir;
        } else {
            wallJumpDir = player.FaceingDir;
        }
    }








}
