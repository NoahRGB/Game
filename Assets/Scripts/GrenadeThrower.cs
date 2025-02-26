using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrower : MonoBehaviour {

    public float throwForce = 10.0f;
    public GameObject grenadeToThrow;

    private Camera cam;


    void Start() {
        cam = Camera.main;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            throwGrenade();
        }
    }

    void throwGrenade() {
        GameObject grenade = Instantiate(grenadeToThrow, cam.transform.position, cam.transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(cam.transform.forward * throwForce, ForceMode.VelocityChange);
    }
}
