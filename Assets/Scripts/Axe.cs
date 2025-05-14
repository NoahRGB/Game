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
    public bool combo1Ready = true;
    public bool combo2Ready = true;
    public bool combo3Ready = true;
    
    private AudioSource audioSource;
    private Animator animator;
    private TMP_Text ammoText;
    private Player player;

    void Start() { 
        animator = GetComponent<Animator>();
        ammoText = GameObject.Find("AmmoCountUI").GetComponent<TMP_Text>();
        ammoText.text = "";
        audioSource = GetComponent<AudioSource>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update() {

        if (!player.inMenu && Input.GetMouseButtonDown(0)) {

            // allows the player to use 1, 2 or 3 attacks in a row
            if (combo1Ready && combo2Ready && combo3Ready && !isAttacking) {
                // assume that the player just wants 1 attack, reset combos
                animator.SetBool("Combo1", false);
                animator.SetBool("Combo2", false);
                animator.SetTrigger("Attack");

            } else if (combo2Ready && combo3Ready && isAttacking && !animator.GetBool("Combo1")) {
                // if the player is attacking and hasn't already queued a combo, then start attack 2
                animator.SetBool("Combo1", true);

            } else if (combo3Ready && isAttacking && !animator.GetBool("Combo2")) {
                // if the player is attacking and hasn't already queued a third combo, then start attack 3
                animator.SetBool("Combo2", true);
            }
        }
    }

    void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.tag == "Enemy") {
            LifeController lifeController = collision.transform.GetComponent<LifeController>();
            if (lifeController != null) {
                if (isAttacking && lifeController.health > 0) {
                    audioSource.PlayOneShot(slashSound);
                    lifeController.TakeDamage(damage);
                }
            }
        }
    }

    public void BeginSwing1() { 
        StartCoroutine(resetCombo1Attack());
        audioSource.PlayOneShot(swingSound1); 
    }

    public void BeginSwing2() { 
        StartCoroutine(resetCombo2Attack());
        audioSource.PlayOneShot(swingSound2); 
    }

    public void BeginSwing3() {
        StartCoroutine(resetCombo3Attack());
        audioSource.PlayOneShot(swingSound3); 
    }

    IEnumerator resetCombo1Attack() {
        combo1Ready = false;
        yield return new WaitForSeconds(attackCooldown);
        combo1Ready = true;
    }

    IEnumerator resetCombo2Attack() {
        combo2Ready = false;
        yield return new WaitForSeconds(attackCooldown);
        combo2Ready = true;
    }

    IEnumerator resetCombo3Attack() {
        combo3Ready = false;
        yield return new WaitForSeconds(attackCooldown);
        combo3Ready = true;
    }
}
