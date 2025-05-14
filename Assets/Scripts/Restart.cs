using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {

    WaveController waveController;

    void Start() {
        waveController = GameObject.Find("WaveController").GetComponent<WaveController>();
    }

    public void RestartGame() {
        waveController.RestartGame();
        SceneManager.LoadScene("Level 1");
    }

    void OnDestroy() {
    }
}
