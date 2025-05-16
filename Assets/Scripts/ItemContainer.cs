using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour {

    private Animator animator;
    private Movement movement;
    private Player player;

    void Start() {
        animator = GetComponent<Animator>();
        
        GameObject playerObj = GameObject.Find("Player");
        movement = playerObj.GetComponent<Movement>();
        player = playerObj.GetComponent<Player>();
    }

    void Update() {
        if (!player.inMenu) {
            float horizontalMove = Input.GetAxis("Horizontal");
            float verticalMove = Input.GetAxis("Vertical");

            // if the player is moving & sprinting, speed up the view bobbing animations
            if (horizontalMove != 0 || verticalMove != 0) {
                animator.SetBool("Moving", true);
                if (movement.isSprinting) {
                    animator.SetFloat("Speed", 2.0f);
                } else {
                    animator.SetFloat("Speed", 1.0f);
                }
            } else {
                animator.SetBool("Moving", false);
            }
        } else {
            animator.SetBool("Moving", false);
        }
    }
}
