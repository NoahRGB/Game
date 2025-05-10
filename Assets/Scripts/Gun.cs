using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gun : MonoBehaviour {

    public GameObject hitEffect;
    public ParticleSystem muzzleFlash;
    public AudioClip shootSound;
    public AudioClip reloadSound;
    public AudioClip hitmarkerSound;

    private AudioSource audioSource;
    private Animator animator;
    private Camera cam;
    private TMP_Text ammoText;
    private Player player;

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
        player = GameObject.Find("Player").GetComponent<Player>();

        UpdateUI();
    }

    void Update() {
        animator.SetBool("Shoot", false);
        animator.SetBool("Reload", false);

        if (!player.inMenu) {
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

            if (hit.transform.tag == "Enemy") {
                if (hit.transform.GetComponent<MeleeEnemy>().isDead) return;
            }

            GameObject effect = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(effect, 2.0f);

            LifeController lifeController = hit.transform.GetComponent<LifeController>();
            if (lifeController != null) {
                audioSource.PlayOneShot(hitmarkerSound);
                lifeController.TakeDamage(damage);
            }
        }

        UpdateUI();
    }

    void UpdateUI() {
        ammoText.text = $"{currentMagazine}/{magazineCapacity}";
    }
}
