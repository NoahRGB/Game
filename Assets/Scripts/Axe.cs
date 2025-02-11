using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour {

    public float damage = 10.0f;
    private float iTime = 1.0f;
    public bool isAttacking = false;
    [SerializeField] private Dictionary<GameObject, float> cooldowns;
    private Animator animator;

    void Start() { 
        cooldowns = new Dictionary<GameObject, float>();
        animator = GetComponent<Animator>();
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
            if (Time.time - cooldowns[key] >= 1.0f) {
                toDelete.Add(key);
                continue;
            }
        }
        foreach (GameObject key in toDelete) {
            cooldowns.Remove(key);
        }
    }

    void OnCollisionEnter(Collision collision) {

        LifeController lifeController = collision.transform.GetComponent<LifeController>();
        if (lifeController != null) {
            if (isAttacking) {
                if (!cooldowns.ContainsKey(collision.gameObject)) {
                    lifeController.takeDamage(damage);
                    cooldowns[collision.gameObject] = Time.time;
                }
                
            }
        }
    }

    
}
