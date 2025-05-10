using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public List<GameObject> allItems = new List<GameObject>();
    public List<string> currentItems = new List<string>();
    public string currentItem;

    Transform itemContainer; 

    private TMP_Text weaponText;
    private GameObject magazineUI;
    private List<string> ammoWeapons = new List<string>() { "REVOLVER", "ASSAULT RIFLE" };
    private Player player;

    void Start() {
        magazineUI = GameObject.Find("MagazineUI");
        weaponText = GameObject.Find("WeaponNameUI").GetComponent<TMP_Text>();
        player = GameObject.Find("Player").GetComponent<Player>();

        itemContainer = transform.Find("Item"); 

        currentItems.Add(itemContainer.GetChild(0).gameObject.name);
        currentItem = itemContainer.GetChild(0).gameObject.name;
    }

    void Update() {
        string newWeapon = null;
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            if (currentItems.Count >= 1) {
                newWeapon = currentItems[0];
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            if (currentItems.Count >= 2) {
                newWeapon = currentItems[1];
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            if (currentItems.Count >= 3) {
                newWeapon = currentItems[2];
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            if (currentItems.Count >= 4) {
                newWeapon = currentItems[3];
            }
        }

        if (newWeapon != null) {
            if (currentItem != newWeapon) {
                SwitchWeapon(newWeapon);
            }
        }

        weaponText.text = currentItem;
    }

    void SwitchWeapon(string weaponName) {
        player.SwitchWeapon();

        Destroy(itemContainer.GetChild(0).gameObject);
        Instantiate(allItems.Find(item => item.name == weaponName), itemContainer);
        currentItem = weaponName;

        if (ammoWeapons.Contains(weaponName)) {
            magazineUI.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        } else {
            magazineUI.transform.localScale = Vector3.zero;
        }
    }

    public void AddNewWeapon(GameObject weaponToAdd) {
        currentItems.Add(weaponToAdd.name);
    }
}