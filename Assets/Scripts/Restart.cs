using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {

    Player player;
    WaveController waveController;

    void Start() {
        player = GameObject.Find("Player").GetComponent<Player>();
        waveController = GameObject.Find("WaveController").GetComponent<WaveController>();
    }

    public void RestartGame() {
        waveController.RestartGame();
        SceneManager.LoadScene("Level 1");
    }

    void OnDestroy() {
        if (player != null) {
            player.ResetPosition();
        }
    }
}
