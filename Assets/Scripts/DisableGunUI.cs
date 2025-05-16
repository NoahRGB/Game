using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGunUI : MonoBehaviour {

    private GameObject magazineUI;

    void Start() {
        magazineUI = GameObject.Find("MagazineUI");
        magazineUI.transform.localScale = Vector3.zero;
    }

    void OnDestroy() {
        magazineUI.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
    }
}
