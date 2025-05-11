using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class WaveController : MonoBehaviour {
 
    public GameObject enemy;
    public int maxWaves = 2;
    public bool disableWaves = true;

    private int waveNumber = 0;
    private int currentMaxEnemyCount = 2;
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
    }

    void Update() {
        if (!disableWaves) {
            if (!levelEnded) {
                enemiesRemaining = enemiesContainer.transform.childCount;

                if (enemiesRemaining == 0) {
                    EnableShop();
                }

                enemiesText.text = $"{enemiesRemaining}";
                waveText.text = $"Wave\n{waveNumber} / {maxWaves}";
            }
        } else {
            DisableShop();
        }
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

    public void NextWave() {
        DisableShop();
        if (waveNumber == maxWaves) {
            EndLevel();
        } else {
            waveNumber++;
            currentMaxEnemyCount += 5;
            for (int i = 0; i < currentMaxEnemyCount; i++) {

                // generate a random point in the area
                Vector3 randomPoint = new Vector3(Random.Range(-30.0f, 64.0f), -1.0f, Random.Range(-43.0f, 58.0f));

                NavMeshHit hitResult;
                // find the closest point on the NavMesh to the random point
                if (NavMesh.SamplePosition(randomPoint, out hitResult, 1000.0f, NavMesh.AllAreas)) {
                    // spawn an enemy in at that random position on the NavMesh
                    Instantiate(enemy, new Vector3(hitResult.position.x, 2.1f, hitResult.position.z), Quaternion.identity, enemiesContainer.transform);
                }
        
            }
        }

    }

    void EndLevel() {
        levelEnded = true;
        goal.enableGoal();
    }
}
