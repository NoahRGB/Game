using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackbox : MonoBehaviour {

    public bool hitting = false;
    public string collidedTag = "";

    private void OnTriggerEnter(Collider other) {
        collidedTag = other.tag;
        hitting = true;
    }

    private void OnTriggerExit(Collider other) {
        collidedTag = "";
        hitting = false;
    }
}
