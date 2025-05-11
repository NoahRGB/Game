using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour {

    private Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
    }

    void Update() {
        // float horizontalMove = Input.GetAxis("Horizontal");
        // float verticalMove = Input.GetAxis("Vertical");

        // if (horizontalMove > 0 || verticalMove > 0) {
        //     animator.SetBool("Moving", true);
        // } else {
        //     animator.SetBool("Moving", false);
        // }
    }
}
