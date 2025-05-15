using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    private WaveController waveController;
    private GameObject playerObj;
    private GameObject hud;
    private GameObject shop;
    private Player player;

    void Start() {
        waveController = GameObject.Find("WaveController").GetComponent<WaveController>();
        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Player>();
        hud = GameObject.Find("UI");
        shop = GameObject.Find("Shop");

        playerObj.SetActive(false);
        hud.SetActive(false);
        shop.SetActive(false);
        waveController.gameStarted = false;
    }

    void Update() {
        Cursor.lockState = CursorLockMode.None;
        player.inMenu = true;
    }

    void OnDestroy() {
        playerObj.SetActive(true);
        player.ResetPosition();
        player.inMenu = false;

        hud.SetActive(true);
        shop.SetActive(true);

        waveController.gameStarted = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
