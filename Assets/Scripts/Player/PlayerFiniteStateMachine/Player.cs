using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
#region State Variables
        
    public PlayerStateMachine StateMachine {get; set;}

    [SerializeField] private PlayerData playerData;

    public PlayerIdleState IdleState {get; private set;}
    public PlayerMoveState MoveState {get; private set;}
    public PlayerJumpState JumpState {get; private set;}
    public PlayerInAirState InAirState {get; private set;}
    public PlayerLandState LandState {get; private set;}
    public PlayerWallSlideState WallSlideState{get; private set;}
    public PlayerWallGrabState WallGrabState {get; private set;}
    public PlayerWallClimbState WallClimbState {get; private set;}
    public PlayerWallJumpState WallJumpState {get; private set;}
    public PlayerLedgeClimbState LedgeClimbState {get; private set;}
    public PlayerLadderClimbState LadderClimbState {get; private set;}
    
    #endregion

#region Components
        
    public Animator Anim {get; set;}
    public PlayerInputHandler InputHandler{get; set;}
    public Rigidbody2D RB;
    public BoxCollider2D COL;

    #endregion

#region CheckTransforms
    
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private Transform ladderCheck;
 
#endregion

#region OtherVariables
    
    private Vector2 workspace;     
    public Vector2 CurrentVelocity{get; private set;}
    public int FaceingDir {get; private set;}

#endregion

#region UnityCallBackFunctions
    
    private void Awake() {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, "wallGrab");
        WallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, "wallClimb");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "inAir");
        LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, playerData, "ledgeClimbState");
        LadderClimbState = new PlayerLadderClimbState(this, StateMachine, playerData, "ladderClimb");
    }

    private void Start() {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        StateMachine.Initialize(IdleState);
        RB = GetComponent<Rigidbody2D>();
        COL = GetComponent<BoxCollider2D>();
        FaceingDir = 1;
    }

    private void Update() 
    {
        CurrentVelocity = RB.velocity;
        StateMachine.CurrentState.LogicUpdate();

        Debug.DrawRay(ladderCheck.position, Vector2.up * playerData.ladderCheckDistance, Color.red);
        Debug.DrawRay(ladderCheck.position, Vector2.down * playerData.ladderCheckDistance, Color.magenta);
    }

    private void FixedUpdate() {
        StateMachine.CurrentState.PhysicsUpdate();
    }

#endregion

#region SetFunctions
    
    public void SetVelocityZero()
    {
        RB.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }


    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;

    }

#endregion

#region CheckFunctions

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }

    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * FaceingDir, playerData.wallCheckDistance, playerData.whatIsGround);
    }

    public bool CheckIfTouchingLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.right * FaceingDir, playerData.wallCheckDistance, playerData.whatIsGround);
    }

    public bool CheckIfTouchingWallBack()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * -FaceingDir, playerData.wallCheckDistance, playerData.whatIsGround);
    }

    public bool CheckIfTouchingLadder()
    {
        return Physics2D.Raycast(ladderCheck.position, Vector2.up, playerData.ladderCheckDistance, playerData.whatIsLadder);
    }

    public bool CheckIfLadderUnderPlayer()
    {
        return Physics2D.Raycast(ladderCheck.position, Vector2.down, playerData.ladderCheckDistance, playerData.whatIsLadder);
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FaceingDir)
        {
            Flip();
        }
    }

#endregion

#region OtherFunctions
    
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    private void AnimationFinishedTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    private void Flip()
    {
        FaceingDir *= -1;
        transform.Rotate(0, 180, 0);
    }

    public Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(wallCheck.position, Vector2.right * FaceingDir, playerData.wallCheckDistance, playerData.whatIsGround);
        float xDist = xHit.distance;

        workspace.Set(xDist * FaceingDir, 0f);

        RaycastHit2D yHit = Physics2D.Raycast(ledgeCheck.position + (Vector3)workspace, Vector2.down, ledgeCheck.position.y - wallCheck.position.y, playerData.whatIsGround);
        float yDist = yHit.distance;

        workspace.Set(wallCheck.position.x + (xDist * FaceingDir), ledgeCheck.position.y - yDist);
        return workspace;
    }

#endregion

}


