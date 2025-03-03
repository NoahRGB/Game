using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour {

    private TMP_Text weaponText;

    public GameObject revolver;
    public GameObject axe;
    public GameObject assaultRifle;
    public GameObject grenade;
    public GameObject currentItem;

    void Start() {
        weaponText = GameObject.Find("WeaponNameUI").GetComponent<TMP_Text>();
        checkForMissingItem();
    }

    void Update() {
        checkForMissingItem();
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
            if (currentItem.transform.name != "Assault Rifle") {
                switchWeapon("AssaultRifle", assaultRifle);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            if (currentItem.transform.name != "Grenade") {
                switchWeapon("Grenade", grenade);
            }
        }

        weaponText.text = currentItem.transform.name;
    }

    void checkForMissingItem() {
        Transform parentContainer = transform.Find("Item");
        bool hasItem = parentContainer.transform.childCount != 0;
        if (hasItem) {
            currentItem = parentContainer.transform.GetChild(0).gameObject;
        } else {
            Instantiate(revolver, parentContainer.transform);
            currentItem = parentContainer.transform.GetChild(0).gameObject;
        }
    }

    void switchWeapon(string weaponName, GameObject weaponObject) {
        Destroy(currentItem);
        currentItem = Instantiate(weaponObject, transform.Find("Item"));
        currentItem.transform.name = weaponName;
    }
}
