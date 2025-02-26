using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2 : MonoBehaviour {

    public float walkSpeed = 5.0f;
    public float friction = 1.0f;
    public float floorCheckDistance = 1.4f;

    private Rigidbody rb;
    private Vector3 movementDir;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        movementDir = transform.right * horizontalMove + transform.forward * verticalMove;

        if (isGrounded()) {
            rb.drag = friction;
        } else {
            rb.drag = 0;
        }

    }

    void FixedUpdate() {
        rb.AddForce(movementDir * walkSpeed * Time.deltaTime, ForceMode.VelocityChange);
    }

    // shoots a ray into the floor (Vector3.down) by floorCheckDistance to check for a hit
    bool isGrounded() {
        return Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, floorCheckDistance);
    }
}

