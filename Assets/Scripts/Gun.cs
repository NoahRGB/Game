using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public ParticleSystem muzzleFlash;
    private Animator animator;
    public Camera cam;

    public int magazineCapacity = 6;
    public float shootDelay = 1.0f;
    public float range = 100.0f;
    private float lastShotTime = 0.0f;
    private int currentMagazine = 6;

    void Start() {
        currentMagazine = magazineCapacity;
        animator = GetComponent<Animator>();
    }

    void Update() {
        animator.SetBool("Shoot", false);
        animator.SetBool("Reload", false);

        if (Input.GetMouseButton(0) && Time.time - lastShotTime >= shootDelay) {
            if (currentMagazine != 0) {
                currentMagazine--;
                Shoot();
                lastShotTime = Time.time;
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            reload();
        }
    }

    void reload() {
        currentMagazine = magazineCapacity;
        animator.SetBool("Reload", true);
    }

    void Shoot() {
        animator.SetBool("Shoot", true);
        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range)) {

        }
    }
}
