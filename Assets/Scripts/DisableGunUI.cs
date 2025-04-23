using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGunUI : MonoBehaviour {

    private GameObject magazineUI;

    void Start() {
        magazineUI = GameObject.Find("MagazineUI");
        magazineUI.SetActive(false);
    }

    void OnDestroy() {
        magazineUI.SetActive(true);
    }
}
