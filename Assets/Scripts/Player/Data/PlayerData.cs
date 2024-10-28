using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "Data/PlayerData/BaseData")]
public class PlayerData : ScriptableObject 
{
    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public float jumpVelocity = 15f;
    public int amountOfJumps = 1;

    [Header("CheckVariables")]
    public float groundCheckRadius = 0.3f;
    public LayerMask whatIsGround;
    public float wallCheckDistance = 0.5f;
    
    public LayerMask whatIsLadder;
    public float ladderCheckDistance = 0.2f;

    [Header("In Air State")]
    public float coyoteTime = 0.15f;
    public float variableJumpHieghtMultiplier = 0.5f;

    [Header("Wall Slide State")]
    public float wallSlideVeocity = 3f;

    [Header("Wall Climb State")]
    public float wallClimbVelocity = 3f;

    [Header("Wall Jump State")]
    public float wallJumpVelocity = 20f;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);
    public float wallJumpCoyoteTime = 0.15f;

    [Header("Ledge Climb State")]
    public Vector2 startOffset;
    public Vector2 stopOffset;

    [Header("Ladder Climb State")]
    public float ladderClimbSpeed = 3f;


}
