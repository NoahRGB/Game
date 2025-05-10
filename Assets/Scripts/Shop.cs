using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    public GameObject cardPrefab;

    private Inventory playerInventory;


    void Start() {
        playerInventory = GameObject.Find("Player").GetComponent<Inventory>();
        
    }

    void Update() {
        
    }

    public void RefreshShop() {
        List<GameObject> chosenItems = new List<GameObject>();
        for (int i = 0; i < 3; i++) {
            GameObject chosenItem = null;
            while (!chosenItems.Contains(chosenItem)) {
                chosenItem = playerInventory.allItems[Random.Range(0, playerInventory.allItems.Count)];
            }
            chosenItems.Add(chosenItem);
        }

        

    }
}
