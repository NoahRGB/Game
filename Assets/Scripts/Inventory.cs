using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public List<GameObject> allItems = new List<GameObject>();
    public List<string> currentItems = new List<string>();
    public Dictionary<string, int> ammoCounts = new Dictionary<string, int>();
    public Dictionary<string, int> magazineCounts = new Dictionary<string, int>();
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
        ToggleAmmoUI();
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

    public void SetAmmo(string weaponName, int newMagazine, int newAmmo) {
        magazineCounts[weaponName] = newMagazine;
        ammoCounts[weaponName] = newAmmo;
    }

    void SwitchWeapon(string weaponName) {
        player.SwitchWeapon();

        Destroy(itemContainer.GetChild(0).gameObject);
        GameObject weapon = Instantiate(allItems.Find(item => item.name == weaponName), itemContainer);
        weapon.name = weaponName;
        currentItem = weaponName;

        Gun gunObj = weapon.GetComponent<Gun>();
        if (gunObj != null) {
            gunObj.SetupAmmo(magazineCounts[weaponName], ammoCounts[weaponName]);
        }

        Grenade grenadeObj = weapon.GetComponent<Grenade>();
        if (grenadeObj != null) {
            grenadeObj.SetupAmmo(ammoCounts[weaponName]);
        }

        ToggleAmmoUI();
    }

    public void AddNewWeapon(GameObject weaponToAdd) {
        currentItems.Add(weaponToAdd.name);

        if (weaponToAdd.name == "ASSAULT RIFLE") {
            ammoCounts[weaponToAdd.name] = 50;
            magazineCounts[weaponToAdd.name] = 30;
        } else if (weaponToAdd.name == "REVOLVER") {
            ammoCounts[weaponToAdd.name] = 40;
            magazineCounts[weaponToAdd.name] = 6;
        } else if (weaponToAdd.name == "GRENADE") {
            ammoCounts[weaponToAdd.name] = 3;
            magazineCounts[weaponToAdd.name] = 1;
        }
    }

    void ToggleAmmoUI() {
        if (ammoWeapons.Contains(currentItem)) {
            magazineUI.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        } else {
            magazineUI.transform.localScale = Vector3.zero;
        }
    }
}