using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    public Camera cam;

    void Start() {
        //cam = GetComponentInChildren<Camera>();
    }

    void Update() {
        
    }

    void OnCollisionEnter(Collision collision) {
        Debug.Log("Moving");
        collision.gameObject.transform.Translate(cam.transform.position -  transform.position);
    }
}
