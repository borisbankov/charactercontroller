using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float Speed = 5f;
    public float JumpHeight = 2f;
    public float Gravity = -9.81f;
    public float GroundDistance = 2f;
    public LayerMask Ground;
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded = true;
    private Transform groundChecker;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        groundChecker = transform.GetChild(0);
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);
        if (isGrounded && velocity.y < 0)
        { 
            velocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        // vector(-1, 0, 1) 
        controller.Move(move * Time.deltaTime * Speed);

        if (move != Vector3.zero)
        { 
            transform.forward = move;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y += Mathf.Sqrt(JumpHeight * -2f * Gravity); 
        }

        velocity.y += Gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

}
