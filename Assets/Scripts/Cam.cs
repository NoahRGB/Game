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

        player = GameObject.Find("Player");
    }

    void Update() {
        bool followPlayer = !player.GetComponent<Player>().inMenu;
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // change camera rotations based on user input
        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90.0f, 90.0f);
        yRot += mouseX;

        if (followPlayer) {
            // rotate the camera, player and item by the new rotations
            transform.localRotation = Quaternion.Euler(xRot, 0.0f, 0.0f);
            item.transform.localRotation = Quaternion.Euler(xRot, 0.0f, 0.0f);
            player.transform.localRotation = Quaternion.Euler(0.0f, yRot, 0.0f);
        }
    }
}
