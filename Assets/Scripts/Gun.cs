using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gun : MonoBehaviour {

    public ParticleSystem muzzleFlash;
    public AudioClip shootSound;
    public AudioClip reloadSound;

    private AudioSource audioSource;
    private Animator animator;
    private Camera cam;
    private TMP_Text ammoText;

    public int magazineCapacity = 6;
    public float shootDelay = 1.0f;
    public float range = 100.0f;
    public float damage = 5.0f;
    private float lastShotTime = 0.0f;
    private int currentMagazine = 6;

    void Start() {
        currentMagazine = magazineCapacity;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        animator = GetComponent<Animator>();
        ammoText = GameObject.Find("AmmoCountUI").GetComponent<TMP_Text>();
        audioSource = GetComponent<AudioSource>();
        UpdateUI();
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
            Reload();
        }
    }

    void Reload() {
        currentMagazine = magazineCapacity;
        animator.SetBool("Reload", true);
        if (audioSource != null) {
            audioSource.PlayOneShot(reloadSound);
        }
        UpdateUI();
    }

    void Shoot() {
        animator.SetBool("Shoot", true);
        muzzleFlash.Play();
        if (audioSource != null) {
            audioSource.PlayOneShot(shootSound);
        }

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range)) {

            LifeController lifeController = hit.transform.GetComponent<LifeController>();
            if (lifeController != null) {
                lifeController.takeDamage(damage);
            }
        }

        UpdateUI();
    }

    void UpdateUI() {
        ammoText.text = $"{currentMagazine}/{magazineCapacity}";
    }
}
