using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public List<GameObject> allItems = new List<GameObject>(); // holds all possible weapons the player can have
    public List<string> currentItems = new List<string>(); // holds the weapons the player currently has unlocked
    public Dictionary<string, int> ammoCounts = new Dictionary<string, int>(); // stores a weapon name alongside how much reserve ammo the player has
    public Dictionary<string, int> magazineCounts = new Dictionary<string, int>(); // stores a weapon name alongisde how much ammo is in the magainze
    public string currentItem;

    Transform itemContainer; 

    private TMP_Text weaponText;
    private GameObject magazineUI;
    private List<string> ammoWeapons = new List<string>() { "REVOLVER", "ASSAULT RIFLE" }; // weapons that need special ammo UI
    private Player player;

    void Start() {
        magazineUI = GameObject.Find("MagazineUI");
        weaponText = GameObject.Find("WeaponNameUI").GetComponent<TMP_Text>();
        player = GameObject.Find("Player").GetComponent<Player>();

        itemContainer = transform.Find("Item"); 

        // add the current weapon to current items
        currentItems.Add(itemContainer.GetChild(0).gameObject.name);
        currentItem = itemContainer.GetChild(0).gameObject.name;
        ToggleAmmoUI();
    }

    void Update() {
        string newWeapon = null;
        // if the player presses the number keys and has enough weapons, then switch to that weapon
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

    public void RefillAmmo() {
        // set each unlocked weapon to maximum ammo
        foreach (string item in currentItems) {
            GameObject weaponObj = allItems.Find(itemObj => itemObj.name == item);
            MaxAmmo(weaponObj.name);
            ResetAmmo(weaponObj);
        }
    }

    public void ResetItems() {
        // removes all items and gives the player an axe
        List<int> itemsToRemove = new List<int>();
        for (int i = 0; i < currentItems.Count; i++) {
            if (currentItems[i] != "AXE") {
                itemsToRemove.Add(i);
            }
        }

        foreach (int index in itemsToRemove) {
            currentItems.Remove(currentItems[index]);
        }

        SwitchWeapon("AXE");
    }

    public void SetAmmo(string weaponName, int newMagazine, int newAmmo) {
        // sets the ammo/magazine counts for the specified weapon
        magazineCounts[weaponName] = newMagazine;
        ammoCounts[weaponName] = newAmmo;
    }

    void SwitchWeapon(string weaponName) {
        // destroys the current weapon object and instantiates a new one
        player.SwitchWeapon();

        Destroy(itemContainer.GetChild(0).gameObject);
        GameObject weapon = Instantiate(allItems.Find(item => item.name == weaponName), itemContainer);
        weapon.name = weaponName;
        currentItem = weaponName;

        ResetAmmo(weapon);
        ToggleAmmoUI();
    }

    private void ResetAmmo(GameObject weapon) {
        // takes a weapon and sets its ammo based on what is stored in the inventory
        Gun gunObj = weapon.GetComponent<Gun>();
        if (gunObj != null) {
            gunObj.SetupAmmo(magazineCounts[weapon.name], ammoCounts[weapon.name]);
        }

        Grenade grenadeObj = weapon.GetComponent<Grenade>();
        if (grenadeObj != null) {
            grenadeObj.SetupAmmo(ammoCounts[weapon.name]);
        }
    }

    private void MaxAmmo(string weaponName) {
        // resets a weapon to its maximum ammo values
        if (weaponName == "ASSAULT RIFLE") {
            ammoCounts[weaponName] = 70;
            magazineCounts[weaponName] = 30;
        } else if (weaponName == "REVOLVER") {
            ammoCounts[weaponName] = 40;
            magazineCounts[weaponName] = 6;
        } else if (weaponName == "GRENADE") {
            ammoCounts[weaponName] = 3;
            magazineCounts[weaponName] = 1;
        }
    }

    public void AddNewWeapon(GameObject weaponToAdd) {
        currentItems.Add(weaponToAdd.name);
        MaxAmmo(weaponToAdd.name);
    }

    void ToggleAmmoUI() {
        if (ammoWeapons.Contains(currentItem)) {
            magazineUI.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        } else {
            magazineUI.transform.localScale = Vector3.zero;
        }
    }
}