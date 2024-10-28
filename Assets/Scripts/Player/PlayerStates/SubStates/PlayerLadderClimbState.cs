using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class PlayerLadderClimbState : PlayerState
{
    private int yInput;

    public bool isClimbing;
    private bool isDecending;

    public float defaulGravityScale { get; private set; }

    public PlayerLadderClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        isClimbing = true;
        defaulGravityScale = player.RB.gravityScale;
    }

    public override void Exit()
    {
        base.Exit();
        player.RB.gravityScale = defaulGravityScale;
        player.RB.bodyType = RigidbodyType2D.Dynamic;
        isClimbing = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        yInput = player.InputHandler.NormlInputY;

        if (isClimbing)
        {
            player.SetVelocityX(0);
            player.SetVelocityY(0);
            player.RB.bodyType = RigidbodyType2D.Kinematic;

            if (yInput == 1)
            {
                player.SetVelocityZero();
                player.SetVelocityY(playerData.ladderClimbSpeed);
            }
        }

        if (isClimbing && yInput == -1)
        {
            player.SetVelocityY(-playerData.ladderClimbSpeed * 10 * Time.deltaTime);
            stateMachine.ChangeState(player.InAirState);
        }

        if (isClimbing)
        {
            if (player.CheckIfTouchingLadder() == false)
            {
                stateMachine.ChangeState(player.InAirState);
            }
        }

        if (!isClimbing)
        {
            stateMachine.ChangeState(player.InAirState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
