using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float health = 10.0f;

    void Start() {

    }

    void Update() {
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision) {
        Debug.Log("Hit!");
        if (collision.gameObject.name == "Bullet(Clone)") {
            health--;
        }
    }

}
