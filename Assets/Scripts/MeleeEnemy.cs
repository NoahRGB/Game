using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : MonoBehaviour {

    public bool isRunner = true;
    public bool isAttacking = false;
    public float hitDetectionRange = 5.0f;
    public float hitCooldown = 1.0f;
    public bool readyToHit = true;
    public float damage = 5.0f;
    public bool isDead = false;

    private NavMeshAgent agent;
    private GameObject player;
    private Animator animator;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();

        if (animator != null) {
            animator.SetBool("Running", isRunner);
            animator.SetBool("Walking", !isRunner);
        }
    }

    void Update() {
        if (!isDead) {
            if (!isAttacking) {
                agent.SetDestination(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            }

            if (readyToHit && (Vector3.Distance(transform.position, player.transform.position) <= hitDetectionRange)) {
                Attack();
            }
        }
    }

    private void Attack() {
        agent.SetDestination(transform.position);
        animator.SetTrigger("Attack");

        StartCoroutine(AttackCooldown());
    }

    public void Die() {
        agent.SetDestination(transform.position);
        isDead = true;
        animator.SetTrigger("Die");

        if (gameObject.GetComponent<Zombie>() != null) {
            gameObject.GetComponent<Zombie>().die();
        }
        StartCoroutine(DestroySelf());
    }

    private IEnumerator DestroySelf() {
        yield return new WaitForSeconds(5.0f);

        Destroy(gameObject);
    }

    private IEnumerator AttackCooldown() {
        readyToHit = false;

        yield return new WaitForSeconds(hitCooldown);

        readyToHit = true;
    }
}
