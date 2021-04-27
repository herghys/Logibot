using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(InputHandler))]
public class PlayerMovement : MonoBehaviour
{
    private InputHandler input;
    private CharacterController controller;
    public Vector3 direction, velocity, drag;

    [SerializeField]
    private float moveSpeed;
    private float defaultSpeed = 12f;
    [SerializeField]
    private float rotateSpeed = 10f;
    [SerializeField]
    private float JumpHeight = 2f;
    [SerializeField]
    private float Gravity = -9.8f;
    [SerializeField]
    private Camera cam;

    //GroundChecker
    public bool _isGrounded = true;
    private Transform _groundChecker;
    public float GroundDistance = 0.2f;
    public LayerMask Ground;

    private void Awake()
    {
        input = GetComponent<InputHandler>();
        controller = GetComponent<CharacterController>();
        _groundChecker = transform.GetChild(0);
    }

    private void Update()
    {
        //Ground Check
        _isGrounded = Grounded();
        if (Grounded() && velocity.y < 0) 
            velocity.y = 0f;

        //Movement
        direction = new Vector3(input.InputVector.x, 0, input.InputVector.y).normalized;
        var moveVector = MoveTowardsTarget(direction);

        

        controller.Move(moveVector * Time.deltaTime * moveSpeed);
        if (direction != Vector3.zero) 
            transform.forward = direction;

        //Jump State
        if (_isGrounded && RollState())
            if (JumpState())
                velocity.y += Mathf.Sqrt(JumpHeight * -2f * Gravity);
            else input.isJump = false ;

        //Rolling State
        if (RollState()) moveSpeed = 20f; else moveSpeed = defaultSpeed;


        //Physics
        velocity.y += Gravity * Time.deltaTime;

        velocity.x /= 1 + drag.x * Time.deltaTime;
        velocity.y /= 1 + drag.y * Time.deltaTime;
        if (velocity.y < -9.8)
        {
            velocity.y = 0;
        }
        velocity.z /= 1 + drag.z * Time.deltaTime;

        RotateTowardsMovement(moveVector);
        controller.Move(velocity * Time.deltaTime);
    }

    private bool Grounded()
    {
        return Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);
    }

    private Vector3 MoveTowardsTarget(Vector3 targetVector)
    {
        targetVector = Quaternion.Euler(0, cam.gameObject.transform.eulerAngles.y, 0) * targetVector;
        return targetVector;
    }

    private void RotateTowardsMovement(Vector3 movementVector)
    {
        if (movementVector.magnitude == 0) return;

        var rotation = Quaternion.LookRotation(movementVector);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed);
    }

    public bool JumpState()
    {
        return input.isJump;
    }

    public bool RollState()
    {
        if (direction.magnitude != 0)
        {
            return input.isRolling;
        }
        else
        {
            input.isRolling = false;
            return input.isRolling;
        }
    }
}