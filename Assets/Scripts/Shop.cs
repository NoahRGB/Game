using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Shop : MonoBehaviour {

    public GameObject cardPrefab;

    private Inventory playerInventory;


    void Start() {
        playerInventory = GameObject.Find("Player").GetComponent<Inventory>();
        RefreshShop();
    }

    void Update() {
        
    }

    public void RefreshShop() {

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
            GameObject card = Instantiate(cardPrefab, transform.GetChild(0));
            ShopItem itemDetails = item.GetComponent<ShopItem>();
            card.GetComponent<ShopCard>().InitialiseCard(item.name, itemDetails.price, itemDetails.shopIcon);
        }
    }
}
