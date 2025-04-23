using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Axe : MonoBehaviour {

    public AudioClip swingSound;
    public AudioClip slashSound;
    public float damage = 10.0f;
    public float attackCooldown = 1.0f;

    public bool isAttacking = false;
    public bool canAttack = true;

    private float iTime = 0.5f;
    [SerializeField] private Dictionary<GameObject, float> cooldowns;

    private AudioSource audioSource;
    private Animator animator;
    private TMP_Text ammoText;

    void Start() { 
        cooldowns = new Dictionary<GameObject, float>();
        animator = GetComponent<Animator>();
        ammoText = GameObject.Find("AmmoCountUI").GetComponent<TMP_Text>();
        ammoText.text = "";
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        freeCooldowns();

        if (Input.GetMouseButtonDown(0)) {
            if (!isAttacking) {

                if (!audioSource.isPlaying) {
                    audioSource.PlayOneShot(swingSound);
                }
                animator.SetTrigger("Attack");
                StartCoroutine(resetAttackCooldown());
            }
        }
    }

    IEnumerator resetAttackCooldown() {
        yield return new WaitForSeconds(attackCooldown);
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
        if (collision.gameObject.tag == "Enemy") {
            LifeController lifeController = collision.transform.GetComponent<LifeController>();

            if (lifeController != null) {
                if (isAttacking) {
                    if (!cooldowns.ContainsKey(collision.gameObject)) {
                        audioSource.PlayOneShot(slashSound);
                        lifeController.takeDamage(damage);
                        cooldowns[collision.gameObject] = Time.time;
                    }

                }
            }
        }
    }
}
