using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {

    public List<string> levelOrder = new List<string>();
    public Dictionary<string, Vector3> goalPositions = new Dictionary<string, Vector3>();
    WaveController waveController;
    public int currentLevel = 1;

    private Player player;
    private TMP_Text levelText;

    MeshRenderer goalRenderer;
    BoxCollider goalCollider;

    public string nextSceneName;

    void Start() {
        waveController = GameObject.Find("WaveController").GetComponent<WaveController>();
        goalRenderer = GetComponent<MeshRenderer>();
        goalCollider = GetComponent<BoxCollider>();
        player = GameObject.Find("Player").GetComponent<Player>();
        levelText = GameObject.Find("LevelNumberUI").GetComponent<TMP_Text>();
        DisableGoal();

        goalPositions.Add("Level 1", new Vector3(9.9f, 5.57f, -15.4f));
        goalPositions.Add("Level 2", new Vector3(-2.76f, 5.57f, -15.4f));

        SetupNewLevel();
    }

    public void SetupNewLevel() {
        currentLevel++;
        UpdateLevelUI();
        string thisLevel = SceneManager.GetActiveScene().name;
        string nextLevel = (thisLevel == "Level 1") ? "Level 2" : "Level 1";
        nextSceneName = (thisLevel == "Level 1") ? "Level 1" : "Level 2";
        transform.localPosition = goalPositions[nextLevel];
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player") {
            DisableGoal();
            SceneManager.LoadScene(nextSceneName);
            SetupNewLevel();
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

    private void UpdateLevelUI() {
        levelText.text = $"Level\n{currentLevel}";
    }
}
