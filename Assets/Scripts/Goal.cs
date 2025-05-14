using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {

    public List<string> levelOrder = new List<string>();
    public Dictionary<string, Vector3> goalPositions = new Dictionary<string, Vector3>();
    WaveController waveController;
    public int currentLevel = -1;

    MeshRenderer goalRenderer;
    BoxCollider goalCollider;

    public string nextSceneName;

    void Start() {
        waveController = GameObject.Find("WaveController").GetComponent<WaveController>();
        goalRenderer = GetComponent<MeshRenderer>();
        goalCollider = GetComponent<BoxCollider>();
        DisableGoal();

        goalPositions.Add("Level 1", new Vector3(9.9f, 5.57f, -15.4f));
        goalPositions.Add("Level 2", new Vector3(-2.76f, 5.57f, -15.4f));

        SetupNewLevel();
    }

    public void SetupNewLevel() {
        currentLevel++;
        string newLevel = levelOrder[currentLevel];
        transform.localPosition = goalPositions[newLevel];
        nextSceneName = levelOrder[currentLevel + 1];
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.collider.gameObject.name == "Player") {
            SceneManager.LoadScene(nextSceneName);
            waveController.NextLevel();
        }
    }

    public void DisableGoal() {
        goalRenderer.enabled = false;
        goalCollider.enabled = false;
    }

    public void EnableGoal() {
        goalRenderer.enabled = true;
        goalCollider.enabled = true;
    }

}
