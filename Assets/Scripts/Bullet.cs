using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour {
    
    private GameObject outPoint;
    private GameObject outDir;
    private GameObject camera;

    private Vector3 velocity;
    private Vector3 direction;
    private float speed = 1.5f;

    void Start() {
        outPoint = GameObject.Find("Out");
        outDir = GameObject.Find("OutDir");
        camera = GameObject.Find("Main Camera");

        direction = camera.transform.forward;
        direction.Normalize();
    }

    void Update() {
        velocity += direction * speed;
        transform.Translate(velocity * Time.deltaTime);

        if (transform.position.magnitude > 100.0f) {
            Destroy(gameObject);
        }
    }

}
