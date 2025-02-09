using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public bool xMove = false;
    public bool zMove = true;
    public float moveSpeed = 1.0f;
    public float strafeDistance = 20.0f;
    private float distanceMoved = 0.0f;
    private int mod = -1;

    void Update() {
        if (distanceMoved > strafeDistance) {
            distanceMoved = 0;
            mod *= -1;
        }
        
        distanceMoved += moveSpeed;
        transform.Translate(xMove ? moveSpeed * mod : 0.0f, 0.0f, zMove ? moveSpeed * mod : 0.0f);
    }
}
