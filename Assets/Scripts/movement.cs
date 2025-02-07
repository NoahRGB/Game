using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {
    public float moveSpeed = 0.01f;

    void Start() {
        
    }

    void Update() {
        if (Input.GetKey(KeyCode.W)) {
            transform.Translate(new Vector3(moveSpeed, 0.0f, 0.0f));
        }
        if (Input.GetKey(KeyCode.A)) {
            transform.Translate(new Vector3(0.0f, 0.0f, moveSpeed));
        }
        if (Input.GetKey(KeyCode.S)) {
            transform.Translate(new Vector3(-moveSpeed, 0.0f, 0.0f));
        }
        if (Input.GetKey(KeyCode.D)) {
            transform.Translate(new Vector3(0.0f, 0.0f, -moveSpeed));
        }

        //transform.position = Vector3.MoveTowards(transform.position, new Vector3(-1.0f, 1.957329f, -1.0f), 0.01f);
        transform.Translate(Vector3.MoveTowards(transform.position, new Vector3(-1.0f, 1.957329f, -1.0f), 0.01f));
    }
}
