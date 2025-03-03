using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : MonoBehaviour {

    private NavMeshAgent agent;
    private GameObject player;
    private Animator animator;
    private MeleeAttackbox zombieHitbox;

    public float hitCooldown = 2.0f;
    public bool isRunner = false;

    private float nextAttackTime = 0.0f;
    

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        zombieHitbox = GetComponentInChildren<MeleeAttackbox>();

        if (animator != null) {
            animator.SetBool("Running", isRunner);
            animator.SetBool("Walking", !isRunner);
        }
    }

    void Update() {
        if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Zombie_ZombieBite") {
            agent.SetDestination(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        }


        //float velocityMag = agent.desiredVelocity.magnitude;
        //float velocityDiff = Vector3.Magnitude(agent.desiredVelocity - agent.velocity);

        if (Time.time >= nextAttackTime && zombieHitbox.hitting && zombieHitbox.collidedTag == "Player") {
            Hit();
        }
    }

    void Hit() {
        animator.SetTrigger("Attack");
        player.GetComponent<LifeController>().takeDamage(10);
        nextAttackTime = Time.time + hitCooldown;
    }
}
