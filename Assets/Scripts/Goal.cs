using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {

    MeshRenderer goalRenderer;
    BoxCollider goalCollider;

    public string sceneName;

    void Start() {
        goalRenderer = GetComponent<MeshRenderer>();
        goalCollider = GetComponent<BoxCollider>();
        disableGoal();
    }

    void OnCollisionEnter(Collision collision) {
        SceneManager.LoadScene(sceneName);
    }

    public void disableGoal() {
        goalRenderer.enabled = false;
        goalCollider.enabled = false;
    }

    public void enableGoal() {
        goalRenderer.enabled = true;
        goalCollider.enabled = true;
    }

}
