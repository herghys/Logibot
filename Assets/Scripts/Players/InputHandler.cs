using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    //public Joystick _joystick;
    private PlayerInputControl playerInput;
    public Vector2 InputVector { get; set; }
    
    public bool isRolling = false;
    public bool isJump = false;
    public bool isInteracting;

    //public PlayerInput playerInput;
    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }
    private void Awake()
    {
        playerInput = new PlayerInputControl();
    }
    private void Update()
    {
        /*var hor = _joystick.Horizontal;
        var ver = _joystick.Vertical;
        InputVector = new Vector2(hor, ver);*/

        InputVector = playerInput.PlayerMain.Move.ReadValue<Vector2>();
        if (playerInput.PlayerMain.Jump.triggered) TryJump();
        if (playerInput.PlayerMain.Roll.triggered) TryRolling();
    }
    public void TryRolling()
    {
        if (InputVector.magnitude != 0)
        {
            isRolling = !isRolling;
        }
        else isRolling = false;
    }
    public void TryJump()
    {
        isJump = !isJump;
    }
}
