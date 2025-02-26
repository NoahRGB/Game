using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {

    public float delay = 3.0f;
    public float damage = 10.0f;
    public float blastRadius = 5.0f;
    public float explosionForce = 700.0f;
    public float throwForce = 10.0f;
    public GameObject explosion;

    private float timer;
    private bool hasExploded = false;

    void Start() {
        timer = delay;
    }

    void Update() {

        timer -= Time.deltaTime;
        if (!hasExploded && timer <= 0) {
            explode();
        }
    }

    void explode() {
        hasExploded = true;
        Instantiate(explosion, transform.position, transform.rotation);

        Collider[] hitObjects = Physics.OverlapSphere(transform.position, blastRadius);
        foreach (Collider collider in hitObjects) {
            Rigidbody hitRb = collider.GetComponent<Rigidbody>();
            if (hitRb != null) {
                hitRb.AddExplosionForce(explosionForce, transform.position, blastRadius);
            }

            LifeController lifeController = collider.GetComponent<LifeController>();
            if (lifeController != null) {
                lifeController.takeDamage(damage);
            }
        }


        Destroy(gameObject);
    }
}
