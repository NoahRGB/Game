using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class WaveController : MonoBehaviour {
 
    public int maxWaves = 2;
    public bool disableWaves = true;
    public bool gameStarted = false;
    public List<GameObject> includedEnemies = new List<GameObject>();

    private Dictionary<string, int> enemyConfigurations = new Dictionary<string, int>();
    private Dictionary<string, float> enemyScaleRates = new Dictionary<string, float>();

    private int waveNumber = 0;
    private int enemiesRemaining = 0;
    private bool levelEnded = false;

    private Goal goal;
    private GameObject enemiesContainer;
    private TMP_Text waveText;
    private TMP_Text enemiesText;
    private GameObject hud;
    private GameObject shop;
    private Player player;

    void Start() {
        goal = GameObject.Find("Goal").GetComponent<Goal>();
        enemiesContainer = GameObject.Find("Enemies");
        waveText = GameObject.Find("WaveNumberUI").GetComponent<TMP_Text>();
        enemiesText = GameObject.Find("EnemiesRemainingUI").GetComponent<TMP_Text>();
        player = GameObject.Find("Player").GetComponent<Player>();

        hud = GameObject.Find("UI");
        shop = GameObject.Find("Shop");

        enemyScaleRates.Add("ZombieRunner", 2.0f);
        enemyScaleRates.Add("ZombieWalker", 1.5f);
        enemyScaleRates.Add("Wolf", 2.0f);
    }

    void RestartEnemyConfigurations() {
        foreach (GameObject enemy in includedEnemies) {
            enemyConfigurations.Remove(enemy.name);
        }
        enemyConfigurations.Add("ZombieRunner", 5);
        enemyConfigurations.Add("ZombieWalker", 3);
        enemyConfigurations.Add("Wolf", 3);
    }

    void Update() {
        if (!disableWaves && gameStarted) {
            if (!levelEnded) {
                if (enemiesContainer != null) {
                    enemiesRemaining = enemiesContainer.transform.childCount;
                    if (enemiesRemaining == 0) {
                        EnableShop();
                    }

                    enemiesText.text = $"{enemiesRemaining}";
                    waveText.text = $"Wave\n{waveNumber} / {maxWaves}";
                } else {
                    enemiesContainer = GameObject.Find("Enemies");
                }

            }
        } else if (disableWaves) {
            DisableShop();
        }
    }

    public void RestartGame() {
        shop.GetComponent<Shop>().RefreshShop();
        player.ResetGame();
        waveNumber = 0;
        levelEnded = false;
        RestartEnemyConfigurations();
    }

    public void NextLevel() {
        EnableShop();
        waveNumber = 0;
        levelEnded = false;
    }

    void EnableShop() {
        Cursor.lockState = CursorLockMode.None;
        player.inMenu = true;
        hud.SetActive(false);
        shop.SetActive(true);
    }

    void DisableShop() {
        Cursor.lockState = CursorLockMode.Locked;
        player.inMenu = false;
        hud.SetActive(true);
        shop.SetActive(false);
    }

    Vector3 GenerateRandomPointOnNavmesh() {
        // generate random point in the bounds of the map
        Vector3 randomPoint = new Vector3(UnityEngine.Random.Range(-30.0f, 64.0f), -1.0f, UnityEngine.Random.Range(-43.0f, 58.0f));

        NavMeshHit hitResult;
        // find the closest point on the NavMesh to the random point
        if (NavMesh.SamplePosition(randomPoint, out hitResult, 1000.0f, NavMesh.AllAreas)) {
            return hitResult.position;
        }
        return Vector3.zero;
    }

    public void NextWave() {
        DisableShop();
        if (waveNumber == maxWaves) {
            EndLevel();
        } else {
            foreach (GameObject enemyType in includedEnemies) {
                if (enemyConfigurations.ContainsKey(enemyType.name) && enemyScaleRates.ContainsKey(enemyType.name)) {
                    
                    if (waveNumber != 0) {
                        enemyConfigurations[enemyType.name] = (int)Math.Floor(enemyConfigurations[enemyType.name] * enemyScaleRates[enemyType.name]);
                    }

                    Debug.Log($"Spawning in {enemyConfigurations[enemyType.name]} {enemyType.name}");
                    for (int i = 0; i < enemyConfigurations[enemyType.name]; i++) {
                        Vector3 point = GenerateRandomPointOnNavmesh();
                        Instantiate(enemyType, new Vector3(point.x, 2.1f, point.z), Quaternion.identity, enemiesContainer.transform);
                    }
                } else {
                    Debug.Log($"{enemyType.name} has not been configured");
                }
            }
            waveNumber++;
        }
    }

    void EndLevel() {
        levelEnded = true;
        goal.EnableGoal();
    }
}
