using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour 
{
    public Vector2 RawMovementInput {get; private set;}

    public int NormlInputX {get; private set;}
    public int NormlInputY {get; private set;}

    public bool JumpInput {get; private set;}
    public bool JumpInputStop {get; private set;}

    public bool GrabInput {get; private set;}


    [SerializeField] private float inputHoldTime = 0.2f;
    private float jumpInputStartTime;

    private void Update() 
    {
        CheckJumpInputHoldTime();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        if (Mathf.Abs(RawMovementInput.x) > 0.5f)
        {
            NormlInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        } else {
            NormlInputX = 0;
        }

        if (Mathf.Abs(RawMovementInput.y) > 0.5f)
        {
            NormlInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
        } else {
            NormlInputY = 0;
        }

       
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            jumpInputStartTime = Time.time;
        } 

        if (context.canceled)
        {
            JumpInputStop = true;
        }
    }

    public void OnGrabInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GrabInput = true;
        }

        if (context.canceled)
        {
            GrabInput = false;
        }

    }

    public void UseJumpInput() => JumpInput = false;
    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }
}
