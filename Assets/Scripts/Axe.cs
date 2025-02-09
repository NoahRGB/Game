using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour {

    public float damage = 10.0f;
    private bool isAttacking = false;
    private Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
    }

    void Update() {
        isAttacking = animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");
        animator.SetBool("Attack", false);

        if (Input.GetMouseButtonDown(0)) {
            animator.SetBool("Attack", true);
        }
    }

    void OnCollisionEnter(Collision collision) {

        LifeController lifeController = collision.transform.GetComponent<LifeController>();
        if (lifeController != null) {
            if (isAttacking) {
                lifeController.takeDamage(damage);
            }
        }
    }
}
