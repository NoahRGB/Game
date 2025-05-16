using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour {

    void Start() {
        // attach to an object to prevent it from being lost when new levels are loaded
        DontDestroyOnLoad(gameObject);
    }

}
