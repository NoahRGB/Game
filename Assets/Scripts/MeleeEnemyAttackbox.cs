using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyAttackbox : MonoBehaviour {

    public bool currentlyHitting = false;
    private MeleeEnemy parent;

    void Start() {
        parent = GetComponentInParent<MeleeEnemy>();
    }

    private void OnTriggerEnter(Collider other) {
        if (!parent.isDead && parent.isAttacking && other.tag == "Player") {
            LifeController lifeController = other.GetComponent<LifeController>();
            if (lifeController) {
                lifeController.TakeDamage(parent.damage);
            }
        }
    }

}
