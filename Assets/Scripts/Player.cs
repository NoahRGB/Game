using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public GameObject revolver;
    public GameObject axe;
    private GameObject currentItem;


    void Start() {
        currentItem = transform.Find("Item").transform.GetChild(0).gameObject;
    }

    void Update() {
        
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            if (currentItem.transform.name != "Revolver") {
                Destroy(currentItem);
                currentItem = Instantiate(revolver, transform.Find("Item"));
                currentItem.transform.name = "Revolver";
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            if (currentItem.transform.name != "Axe") {
                Destroy(currentItem);
                currentItem = Instantiate(axe, transform.Find("Item"));
                currentItem.transform.name = "Axe";
            }
        }
    }
}
