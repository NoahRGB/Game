using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float walkSpeed = 10.0f;
    public float sprintSpeed = 15.0f;
    public float gravity = -9.8f;
    public float jumpForce = 100.0f;
    public float floorCheckDistance = 1.0f;
    public CharacterController characterController;

    private Vector3 velocity;
    private bool isSprinting = false;

    void Update() {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");
        isSprinting = Input.GetAxis("Sprint") > 0;

        Vector3 movement = transform.right * horizontalMove + transform.forward * verticalMove;
        characterController.Move(movement * (isSprinting ? sprintSpeed : walkSpeed) * Time.deltaTime);

        if (isGrounded()) {

            if (velocity.y < 0) velocity.y = 0.0f; // building up gravity, so reset it
            if (Input.GetButtonDown("Jump")) velocity.y = jumpForce;

        } else {
            velocity.y += gravity;
        }

        characterController.Move(velocity * Time.deltaTime);
    }

    // shoots a ray into the floor (Vector3.down) by floorCheckDistance to check for a hit
    bool isGrounded() {
        return Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, floorCheckDistance);
    }
}
