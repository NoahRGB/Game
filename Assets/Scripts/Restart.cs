using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {

    private WaveController waveController;

    void Start() {
        waveController = GameObject.Find("WaveController").GetComponent<WaveController>();
    }

    public void RestartGame() {
        SceneManager.LoadScene("Level 1");
        waveController.RestartGame();
    }
}
