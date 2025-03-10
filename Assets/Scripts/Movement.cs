using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float walkSpeed = 10.0f;
    public float crouchSpeed = 5.0f;
    public float sprintSpeed = 15.0f;
    public float slideSpeed = 30.0f;
    public float slideDuration = 100.0f;
    public float crouchHeight = 2;
    public float gravity = -9.8f;
    public float jumpForce = 100.0f;
    public float floorCheckDistance = 1.2f;

    public AudioClip jumpSound;

    public GameObject playerBody;
    private AudioSource audioSource;
    private CharacterController characterController;
    private GameObject currentItem;
    private Camera cam;
    private CapsuleCollider capsuleCollider;

    private bool isSprinting = false;
    private bool isCrouching = false;
    private bool isSliding = false;

    private float slideTimer;
    private Vector3 movementDir;
    private Vector3 velocity;

    void Start() {
        characterController = GetComponent<CharacterController>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        audioSource = GetComponent<AudioSource>();
        currentItem = GameObject.Find("Item");
        cam = Camera.main;
    }

    void Update() {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");
        isSprinting = Input.GetAxis("Sprint") > 0;

        movementDir = transform.right * horizontalMove + transform.forward * verticalMove;

        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            startCrouch();
            if (horizontalMove != 0 || verticalMove != 0) {
                startSlide();
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftControl)) {
            stopSlide();
            stopCrouch();
        }

        if (isSliding) {
            characterController.Move(movementDir * slideSpeed * Time.deltaTime);
            slideTimer -= Time.fixedTime;

            if (slideTimer <= 0) {
                stopSlide();
            }
        }

        if (isGrounded()) {
            if (velocity.y < 0) velocity.y = 0.0f; // building up gravity, so reset it
            if (Input.GetButtonDown("Jump")) {
                velocity.y = jumpForce;
                audioSource.PlayOneShot(jumpSound);
            }

        } else { 
            velocity.y += gravity;
        }

        float currentSpeed = isSprinting ? sprintSpeed : isCrouching ? crouchSpeed : walkSpeed;
        characterController.Move(movementDir * currentSpeed * Time.deltaTime);
        characterController.Move(velocity * Time.deltaTime);

    }

    void startSlide() {
        isSliding = true;
    }

    void stopSlide() {
        isSliding = false;
        slideTimer = slideDuration;
    }

    void startCrouch() {
        isCrouching = true;
        playerBody.transform.localScale = new Vector3(transform.localScale.x, crouchHeight, transform.localScale.z);

        cam.transform.Translate(0.0f, -crouchHeight, 0.0f);
        currentItem.transform.Translate(0.0f, -crouchHeight, 0.0f);

        characterController.height = 0.8f;
        capsuleCollider.height = 0.8f;
        floorCheckDistance = 0.2f;

    }
    
    void stopCrouch() {
        isCrouching = false;
        playerBody.transform.localScale = new Vector3(transform.localScale.x, 1.0f, transform.localScale.z);
        cam.transform.Translate(0.0f, crouchHeight, 0.0f);
        currentItem.transform.Translate(0.0f, crouchHeight, 0.0f);

        characterController.height = 2.0f;
        capsuleCollider.height = 2.0f;
        floorCheckDistance = 1.2f;

        characterController.Move(Vector3.one * 0.5f * Time.deltaTime);
    }

    // shoots a ray into the floor (Vector3.down) by floorCheckDistance to check for a hit
    bool isGrounded() {
        return Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, floorCheckDistance);
    }
}
