using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

    public GameObject cardPrefab;

    private Player player;
    private GameObject refillAmmoButton; 
    private Inventory playerInventory;


    void Start() {
        refillAmmoButton = GameObject.Find("AmmoButton");

        GameObject playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Player>();
        playerInventory = playerObj.GetComponent<Inventory>();
        
        refillAmmoButton.GetComponent<Button>().onClick.AddListener(RefillAmmo);
    }

    void Update() {
        
    }

    public void RefillAmmo() {
        if (player.GetCash() >= 100) {
            player.RemoveCash(100);
            playerInventory.RefillAmmo();
        }
    }

    public void RefreshShop() {
        
        GameObject shopBorder = transform.GetChild(0).gameObject;
        foreach (Transform child in shopBorder.transform) {
            if (child.name.Contains("Card")) {
                Destroy(child.gameObject);
            }
        }

        // choose 3 unique items from playerInventory.allItems
        List<GameObject> chosenItems = new List<GameObject>();
        for (int i = 0; i < 3; i++) {
            GameObject chosenItem = playerInventory.allItems[0];
            while (chosenItems.Contains(chosenItem)) {
                chosenItem = playerInventory.allItems[Random.Range(0, playerInventory.allItems.Count)];
            }
            chosenItems.Add(chosenItem);
        }

        // for each of the 3 unique items, instantiate a shop card
        foreach (GameObject item in chosenItems) {
            GameObject card = Instantiate(cardPrefab, shopBorder.transform);
            ShopItem itemDetails = item.GetComponent<ShopItem>();
            card.GetComponent<ShopCard>().InitialiseCard(item.name, itemDetails.price, itemDetails.shopIcon);
        }
    }
}
