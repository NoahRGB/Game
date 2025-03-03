using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Axe : MonoBehaviour {

    public float damage = 10.0f;
    private float iTime = 1.0f;
    public bool isAttacking = false;

    [SerializeField] private Dictionary<GameObject, float> cooldowns;
    private Animator animator;
    private TMP_Text ammoText;

    void Start() { 
        cooldowns = new Dictionary<GameObject, float>();
        animator = GetComponent<Animator>();
        ammoText = GameObject.Find("AmmoCountUI").GetComponent<TMP_Text>();
        ammoText.text = "";
    }

    void Update() {
        isAttacking = animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2");
        animator.SetBool("Attack", false);
        freeCooldowns();

        if (Input.GetMouseButtonDown(0)) {
            animator.SetBool("Attack", true);
        }
    }

    void freeCooldowns() {
        List<GameObject> toDelete = new List<GameObject>();
        foreach (GameObject key in cooldowns.Keys) {
            if (Time.time - cooldowns[key] >= iTime) {
                toDelete.Add(key);
                continue;
            }
        }
        foreach (GameObject key in toDelete) {
            cooldowns.Remove(key);
        }
    }

    void OnCollisionEnter(Collision collision) {
        Debug.Log("Collision detected");
        LifeController lifeController = collision.transform.GetComponent<LifeController>();
        if (lifeController != null) {
            Debug.Log("Found life controller");
            if (isAttacking) {
                Debug.Log("I think i'm attacking");
                if (!cooldowns.ContainsKey(collision.gameObject)) {
                    Debug.Log("No cooldown. Taking damage");
                    lifeController.takeDamage(damage);
                    cooldowns[collision.gameObject] = Time.time;
                }

            }
        }
    }

    
}
