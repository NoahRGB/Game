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
    private int currentMagazine = 6;
    public float range = 100.0f;
    public float damage = 5.0f;
    public float shootCooldown = 1.0f;
    public bool canShoot = true;
    public float reloadCooldown = 0.5f;
    public bool canReload = true;

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
            if (Input.GetMouseButton(0) && canShoot) {
                if (currentMagazine != 0) {
                    currentMagazine--;
                    Shoot();
                    StartCoroutine(resetShootCooldown());
                    StartCoroutine(resetReloadCooldown());
                }
            }

            if (Input.GetKeyDown(KeyCode.R) && canReload) {
                animator.SetBool("Reload", true);
            }
        }

    }

    IEnumerator resetShootCooldown() {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }
    
    IEnumerator resetReloadCooldown() {
        canReload = false;
        yield return new WaitForSeconds(shootCooldown);
        canReload = true;
    }

    public void Reload() {
        currentMagazine = magazineCapacity;
        if (audioSource != null) {
            audioSource.PlayOneShot(reloadSound);
        }
        UpdateUI();
        StartCoroutine(resetReloadCooldown());
    }

    void Shoot() {
        animator.SetBool("Shoot", true);
        muzzleFlash.Play();
        if (audioSource != null) {
            audioSource.PlayOneShot(shootSound);
        }

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range)) {

            // create hit particle effect and destroy it 2 seconds later
            GameObject effect = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(effect, 2.0f);

            // take away health if there is a valid life controller and enemy is not dead
            LifeController lifeController = hit.transform.GetComponent<LifeController>();
            if (lifeController != null && lifeController.health > 0) {
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
