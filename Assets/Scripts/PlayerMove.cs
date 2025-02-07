using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float moveSpeed;
    public float mouseSensitivity;

    private float horizontalMoveInput;
    private float verticalMoveInput;
    private float horizontalMouseInput;
    private float verticalMouseInput;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {

        horizontalMoveInput = Input.GetAxis("Horizontal");
        verticalMoveInput = Input.GetAxis("Vertical");
        horizontalMouseInput = Input.GetAxis("Mouse X");
        verticalMouseInput = Input.GetAxis("Mouse Y");


    }

    private void FixedUpdate() {

        transform.Translate(transform.right * horizontalMoveInput * moveSpeed * Time.deltaTime);
        transform.Translate(transform.forward * verticalMoveInput * moveSpeed * Time.deltaTime);

        transform.Rotate(transform.up * horizontalMouseInput * mouseSensitivity * Time.deltaTime);
        transform.Rotate(transform.up * verticalMouseInput * mouseSensitivity * Time.deltaTime);
    }
}
