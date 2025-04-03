using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    void Start() {
        goal = GameObject.Find("Goal").GetComponent<Goal>();
        enemiesContainer = GameObject.Find("Enemies");
        waveText = GameObject.Find("WaveUI").GetComponent<TMP_Text>();
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

            waveText.text = $"Wave {waveNumber} / {maxWaves}";
        }
    }

    void nextWave() {
        if (waveNumber == maxWaves) {
            endLevel();
        } else {
            waveNumber++;
            currentMaxEnemyCount += 5;
            for (int i = 0; i < currentMaxEnemyCount; i++) {
                Instantiate(enemy, new Vector3(Random.Range(-30.0f, 60.0f), 2.1f, Random.Range(10.0f, 30.0f)), Quaternion.identity, enemiesContainer.transform);
            }
        }

    }

    void endLevel() {
        roundEnded = true;
        goal.enableGoal();
    }
}
