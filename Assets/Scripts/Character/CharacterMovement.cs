using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public CharacterController controller;

    // public float runSpeed; Future implementation
    public float walkSpeed = 50f;
    private float horizontalMovement;
    private bool jump;

    // TODO Add slipery physics material
    void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal") * walkSpeed;
        // If we detech that the jump button is activated, set the variable to true
        jump |= Input.GetAxisRaw("Jump") > 0;
    }

    // Here we call the controller to make the movement of the character
    // using physics
    void FixedUpdate()
    {
        // Horizontal Movement is multiplied by fixedDeltaTime to prevent
        // laggy movement in other systems
        controller.MoveCharacter(horizontalMovement * Time.fixedDeltaTime, jump);
        // After we make the jump we put the boolean with false
        // The controller will prevent the character from jumping again
        // until it touches the ground
        jump = false;
    }
}