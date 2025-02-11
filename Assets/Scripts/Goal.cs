using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {

    public string sceneName;

    void OnCollisionEnter(Collision collision) {
        SceneManager.LoadScene(sceneName);
    }

}
