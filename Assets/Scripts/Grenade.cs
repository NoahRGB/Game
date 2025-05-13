using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Grenade : MonoBehaviour {

    public float throwForce = 10.0f;
    public GameObject grenadeToThrow;
    public AudioClip throwSound;
    public int totalAmmo = 0;
    public float throwCooldown = 1.0f;
    public bool readyToThrow = true;

    private Camera cam;
    private TMP_Text ammoText;
    private Player player;
    private Inventory inventory;
    private AudioSource audioSource;

    void Start() {
        cam = Camera.main;
        ammoText = GameObject.Find("AmmoCountUI").GetComponent<TMP_Text>();
        audioSource = GetComponent<AudioSource>();

        GameObject playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Player>();
        inventory = playerObj.GetComponent<Inventory>();

        ammoText.text = "";
    }

    public void SetupAmmo(int newTotalAmmo) {
        totalAmmo = newTotalAmmo;
    }

    void Update() {
        if (!player.inMenu && Input.GetMouseButtonDown(0) && readyToThrow) {
            throwGrenade();
        }

        UpdateUI();
    }

    void throwGrenade() {
        if (totalAmmo >= 1) {
            StartCoroutine(resetThrowCooldown());
            audioSource.PlayOneShot(throwSound);
            totalAmmo--;
            inventory.SetAmmo(name, 1, totalAmmo);
            GameObject grenade = Instantiate(grenadeToThrow, cam.transform.position, cam.transform.rotation);
            Rigidbody rb = grenade.GetComponent<Rigidbody>();
            rb.AddForce(cam.transform.forward * throwForce, ForceMode.VelocityChange);
        }
    }

    IEnumerator resetThrowCooldown() {
        readyToThrow = false;
        yield return new WaitForSeconds(throwCooldown);
        readyToThrow = true;
    }

    void UpdateUI() {
        string currentMag = totalAmmo == 0 ? "0" : "1";
        ammoText.text = $"{currentMag}/{totalAmmo}";
    }
}
