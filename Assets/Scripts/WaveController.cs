using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {

    public GameObject enemy;

    private Goal goal;
    private GameObject enemiesContainer;

    private int waveNumber = 1;
    private int currentMaxEnemyCount = 10;
    private int enemiesRemaining = 0;

    void Start() {
        goal = GameObject.Find("Goal").GetComponent<Goal>();
        enemiesContainer = GameObject.Find("Enemies");
    }

    void Update() {

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
    }

    void nextWave() {
        waveNumber++;
        currentMaxEnemyCount += 5;

        for (int i = 0; i < currentMaxEnemyCount; i++) {
            Instantiate(enemy, new Vector3(Random.Range(-30.0f, 60.0f), 2.1f, Random.Range(10.0f, 30.0f)), Quaternion.identity, enemiesContainer.transform);
        }
    }




}
