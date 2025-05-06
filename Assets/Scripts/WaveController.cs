using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class WaveController : MonoBehaviour {
 
    public GameObject enemy;
    public int maxWaves = 2;

    private Goal goal;
    private GameObject enemiesContainer;

    private int waveNumber = 0;
    private int currentMaxEnemyCount = 2;
    private int enemiesRemaining = 0;
    private bool roundEnded = false;

    private TMP_Text waveText;
    private TMP_Text enemiesText;

    void Start() {
        goal = GameObject.Find("Goal").GetComponent<Goal>();
        enemiesContainer = GameObject.Find("Enemies");
        waveText = GameObject.Find("WaveNumberUI").GetComponent<TMP_Text>();
        enemiesText = GameObject.Find("EnemiesRemainingUI").GetComponent<TMP_Text>();
    }

    void Update() {

        if (!roundEnded) {
            enemiesRemaining = enemiesContainer.transform.childCount;

            if (enemiesRemaining == 0) {
                nextWave();
            }

            if (Input.GetKeyDown(KeyCode.L)) {
                goal.enableGoal();
            }
            if (Input.GetKeyDown(KeyCode.K)) {
                goal.disableGoal();
            }

            enemiesText.text = $"{enemiesRemaining} enemies remaining";
            waveText.text = $"Wave   {waveNumber} / {maxWaves}";
        }
    }

    void nextWave() {
        if (waveNumber == maxWaves) {
            endLevel();
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

    void endLevel() {
        roundEnded = true;
        goal.enableGoal();
    }
}
