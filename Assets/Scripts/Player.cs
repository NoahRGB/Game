using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public GameObject revolver;
    public GameObject axe;
    public GameObject assaultRifle;
    private GameObject currentItem;


    void Start() {
        currentItem = transform.Find("Item").transform.GetChild(0).gameObject;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            if (currentItem.transform.name != "Revolver") {
                switchWeapon("Revolver", revolver);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            if (currentItem.transform.name != "Axe") {
                switchWeapon("Axe", axe);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            if (currentItem.transform.name != "AssaultRifle") {
                switchWeapon("AssaultRifle", assaultRifle);
            }
        }
    }

    void switchWeapon(string weaponName, GameObject weaponObject) {
        Destroy(currentItem);
        currentItem = Instantiate(weaponObject, transform.Find("Item"));
        currentItem.transform.name = weaponName;
    }
}
