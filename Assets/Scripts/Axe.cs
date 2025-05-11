using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Axe : MonoBehaviour {

    public AudioClip swingSound1;
    public AudioClip swingSound2;
    public AudioClip swingSound3;
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
    private Player player;

    void Start() { 
        cooldowns = new Dictionary<GameObject, float>();
        animator = GetComponent<Animator>();
        ammoText = GameObject.Find("AmmoCountUI").GetComponent<TMP_Text>();
        ammoText.text = "";
        audioSource = GetComponent<AudioSource>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update() {

        freeCooldowns();

        if (!player.inMenu && Input.GetMouseButtonDown(0)) {
            if (!isAttacking) {

                audioSource.PlayOneShot(swingSound1);
                animator.SetTrigger("Attack");
                // StartCoroutine(resetAttackCooldown());

            } else if (isAttacking) {
                animator.SetTrigger("Combo");
                audioSource.PlayOneShot(swingSound2);
            }
        }
    }

    IEnumerator resetAttackCooldown() {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
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
                        lifeController.TakeDamage(damage);
                        cooldowns[collision.gameObject] = Time.time;
                    }

                }
            }
        }
    }
}
