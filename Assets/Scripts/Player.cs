using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    private TMP_Text weaponText;
    private AudioSource audioSource;
    private GameObject magazineUI;

    public AudioClip selectSound;
    public GameObject revolver;
    public GameObject axe;
    public GameObject assaultRifle;
    public GameObject grenade;
    public GameObject currentItem;

    private List<string> ammoWeapons = new List<string>() { "REVOLVER", "ASSAULT RIFLE" };

    void Start() {
        magazineUI = GameObject.Find("MagazineUI");
        weaponText = GameObject.Find("WeaponNameUI").GetComponent<TMP_Text>();
        audioSource = GetComponent<AudioSource>();

        checkForMissingItem();
    }

    void Update() {
        checkForMissingItem();
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            if (currentItem.transform.name != "REVOLVER") {
                switchWeapon("REVOLVER", revolver);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            if (currentItem.transform.name != "AXE") {
                switchWeapon("AXE", axe);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            if (currentItem.transform.name != "ASSAULT RIFLE") {
                switchWeapon("ASSAULT RIFLE", assaultRifle);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            if (currentItem.transform.name != "GRENADE") {
                switchWeapon("GRENADE", grenade);
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
            switchWeapon("REVOLVER", revolver);
        }
    }

    void switchWeapon(string weaponName, GameObject weaponObject) {
        audioSource.PlayOneShot(selectSound);
        Destroy(currentItem);
        currentItem = Instantiate(weaponObject, transform.Find("Item"));
        currentItem.transform.name = weaponName;

        if (ammoWeapons.Contains(weaponName)) {
            magazineUI.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        } else {
            magazineUI.transform.localScale = Vector3.zero;
        }
    }
}
