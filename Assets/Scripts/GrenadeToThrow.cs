using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeToThrow : MonoBehaviour {

    public float delay = 3.0f;
    public float damage = 10.0f;
    public float blastRadius = 5.0f;
    public float explosionForce = 700.0f;
    public float throwForce = 10.0f;
    public GameObject explosion;
    public AudioClip explosionSound;
    private AudioSource audioSource;

    private float timer;
    private bool hasExploded = false;

    void Start() {
        timer = delay;
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        // tick the timer down every frame, then explode
        timer -= Time.deltaTime;
        if (!hasExploded && timer <= 0) {
            Explode();
        }
    }

    void Explode() {
        // play explosion sound and instantiate explosion effect
        hasExploded = true;
        audioSource.PlayOneShot(explosionSound);
        Instantiate(explosion, transform.position, transform.rotation);

        // use overlap sphere to get a list of objects in blast radius around the grenade
        Collider[] hitObjects = Physics.OverlapSphere(transform.position, blastRadius);
        foreach (Collider collider in hitObjects) {
            // if the object has a rigidbody, then apply some force to push it
            Rigidbody hitRb = collider.GetComponent<Rigidbody>();
            if (hitRb != null) {
                hitRb.AddExplosionForce(explosionForce, transform.position, blastRadius);
            }

            // if the object has a life controller then take away health
            LifeController lifeController = collider.GetComponent<LifeController>();
            if (lifeController != null) {
                lifeController.TakeDamage(damage);
            }
        }

        // destroy the used grenade on the ground
        Destroy(gameObject, 3);
    }
}
