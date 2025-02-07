using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {

    public float followDistance;
    public float followHeight;

    public GameObject followObject;


    void Start() {
        //followObject = GameObject.Find("Player");
    }

    void Update() {
        transform.position = new Vector3(followObject.transform.position.x, followHeight, followObject.transform.position.z - followDistance);
    }
}
