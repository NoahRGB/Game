using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour {

    public float health = 20.0f;

    public void takeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            die();
        }
    }

    private void die() {
        Destroy(gameObject);
    }
}
