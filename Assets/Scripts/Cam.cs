using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour {
    public float sensitivity;
    public GameObject player;
    

    private GameObject item;
    private float xRot = 0.0f;
    private float yRot = 0.0f;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        item = GameObject.Find("Item");
    }

    void Update() {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90.0f, 90.0f);

        yRot += mouseX;

        transform.localRotation = Quaternion.Euler(xRot, 0.0f, 0.0f);
        item.transform.localRotation = Quaternion.Euler(xRot, 0.0f, 0.0f);
        //item.transform.Rotate(xRot, yRot, 0.0f);
        player.transform.localRotation = Quaternion.Euler(0.0f, yRot, 0.0f);
    }
}
