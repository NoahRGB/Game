using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class ShopCard : MonoBehaviour {

    public string itemName;
    public float itemPrice;
    public bool isAlreadyOwned = false;
    public AudioClip purchaseSound;

    private Player player;
    private Inventory playerInventory;
    private AudioSource audioSource;
    
    private TMP_Text nameUI;
    private TMP_Text costUI;
    private GameObject selectButtonUI;

    void Start() {
        player = GameObject.Find("Player").GetComponent<Player>();
        playerInventory = GameObject.Find("Player").GetComponent<Inventory>();
        selectButtonUI.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(Purchase);
        audioSource = GetComponent<AudioSource>();
    }

    public void Purchase() {
        // if player has enough money, give them the weapon
        if (player.GetCash() >= itemPrice) {
            player.RemoveCash(itemPrice);
            playerInventory.AddNewWeapon(playerInventory.allItems.Find(item => item.name == itemName));
            audioSource.PlayOneShot(purchaseSound);
        }
    }

    public void InitialiseCard(string name, float price, GameObject icon) {
        // set the various UI elements of the card
        foreach (Transform child in transform) {
            if (child.name.Contains("Name")) {
                nameUI = child.GetComponent<TMP_Text>();
            } else if (child.name.Contains("Cost")) {
                costUI = child.GetComponent<TMP_Text>();
            } else if (child.name.Contains("Button")) {
                selectButtonUI = child.gameObject;
            }
        }

        // create the provided icon (the picture of the item on the card)
        Instantiate(icon, transform);

        nameUI.text = name;
        costUI.text = $"${price}";

        itemName = name.ToUpper();
        itemPrice = price;
    }

    void Update() {
        if (!isAlreadyOwned) {
            isAlreadyOwned = playerInventory.currentItems.Exists(item => item == itemName.ToUpper());

            if (isAlreadyOwned) { // must have just been purchased, so disable the button
                DisableCardButton();
            }
        }
    }

    void DisableCardButton() {
        // sets the SELECT button to red and disables it
        selectButtonUI.GetComponent<UnityEngine.UI.Button>().interactable = false;
        selectButtonUI.GetComponent<UnityEngine.UI.Image>().color = Color.red;
        selectButtonUI.GetComponentInChildren<TMP_Text>().text = "OWNED";
    }

}
