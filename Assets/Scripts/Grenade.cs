using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Grenade : MonoBehaviour {

    public float throwForce = 10.0f;
    public GameObject grenadeToThrow;

    private Camera cam;
    private TMP_Text ammoText;
    private Player player;

    void Start() {
        cam = Camera.main;
        ammoText = GameObject.Find("AmmoCountUI").GetComponent<TMP_Text>();
        player = GameObject.Find("Player").GetComponent<Player>();
        ammoText.text = "";
    }

    void Update() {
        if (!player.inMenu && Input.GetMouseButtonDown(0)) {
            throwGrenade();
        }
    }

    void throwGrenade() {
        GameObject grenade = Instantiate(grenadeToThrow, cam.transform.position, cam.transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(cam.transform.forward * throwForce, ForceMode.VelocityChange);
    }
}
