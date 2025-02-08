using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public GameObject bulletPrefab;
    private GameObject bulletOrigin;

    void Start() {
        bulletOrigin = GameObject.Find("Out");
    }

    void Update() {

        if (Input.GetMouseButtonDown(0)) {
            releaseBullet();
        }
    }

    void releaseBullet() {
        GameObject newBullet = Instantiate(bulletPrefab);
        newBullet.transform.position = bulletOrigin.transform.position;

    }
}
