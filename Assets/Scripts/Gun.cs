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
    public AudioClip emptyShootSound;

    private AudioSource audioSource;
    private Animator animator;
    private Camera cam;
    private TMP_Text ammoText;
    private Player player;
    private Inventory inventory;

    public int magazineCapacity = 6;
    private int currentMagazine = 6;
    private int totalAmmo = 0;
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

        GameObject playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Player>();
        inventory = playerObj.GetComponent<Inventory>();

        if (inventory.magazineCounts.ContainsKey(name)) {
            currentMagazine = inventory.magazineCounts[name];
        }

        if (inventory.ammoCounts.ContainsKey(name)) {
            totalAmmo = inventory.ammoCounts[name];
        }

        UpdateUI();
    }

    void Update() {
        animator.SetBool("Shoot", false);
        animator.SetBool("Reload", false);

        if (!player.inMenu) {
            if (Input.GetMouseButton(0) && canShoot) {
                if (currentMagazine != 0) {
                    currentMagazine--;
                    inventory.SetAmmo(name, currentMagazine, totalAmmo);
                    Shoot();
                } else {
                    audioSource.PlayOneShot(emptyShootSound);
                }
                StartCoroutine(resetShootCooldown());
                StartCoroutine(resetReloadCooldown());
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

    public void SetupAmmo(int currentMag, int currentAmmo) {
        currentMagazine = currentMag;
        totalAmmo = currentAmmo;
    }

    public void Reload() {
        int ammoToReload = magazineCapacity - currentMagazine;

        currentMagazine += (totalAmmo >= ammoToReload) ? ammoToReload : totalAmmo;
        totalAmmo -= (totalAmmo >= ammoToReload) ? ammoToReload : totalAmmo;;
        if (audioSource != null) {
            audioSource.PlayOneShot(reloadSound);
        }

        UpdateUI();
        StartCoroutine(resetShootCooldown());
        StartCoroutine(resetReloadCooldown());
        inventory.SetAmmo(name, currentMagazine, totalAmmo);
    }

    void Shoot() {
        animator.SetBool("Shoot", true);
        muzzleFlash.Play();
        audioSource.PlayOneShot(shootSound);
        
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
        ammoText.text = $"{currentMagazine}/{totalAmmo}";
    }
}
