using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;

    private bool jumpInput;
    private bool grabInput;
    private bool isGrounded;
    protected bool isTouchingWall;
    protected bool isTouchingLadderUp;
    protected bool isUnderLadder;
    protected bool isClimbing;
    private bool isTouchingLegde;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isTouchingWall = player.CheckIfTouchingWall();
        isGrounded = player.CheckIfGrounded();
        isTouchingLadderUp = player.CheckIfTouchingLadder() && player.CheckIfLadderUnderPlayer();
        isUnderLadder = player.CheckIfLadderUnderPlayer();
        isTouchingLegde = player.CheckIfTouchingLedge();
    }

    public override void Enter()
    {
        base.Enter();
        player.JumpState.ResetAmountOfJumps();
        isClimbing = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        xInput = player.InputHandler.NormlInputX;
        jumpInput = player.InputHandler.JumpInput;
        grabInput = player.InputHandler.GrabInput;


        if (jumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if (!isGrounded)
        {
            stateMachine.ChangeState(player.InAirState);
            player.InAirState.StartCoyoteTime();
        }
        else if (isTouchingWall && grabInput && isTouchingLegde)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
        else if (isTouchingLadderUp && (player.InputHandler.NormlInputY == 1) && !isClimbing)
        {
            stateMachine.ChangeState(player.LadderClimbState);
            isClimbing = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}//class
