using UnityEngine;
using UnityEngine.AI;

public class EnemyPathfind : MonoBehaviour {

    private NavMeshAgent agent;
    private GameObject player;
    private Animator animator;

    public float runSpeedThreshold = 0.5f;
    public float idleSpeedThreshold = 0.01f;

    private bool isRunning = false;
    private bool isWalking = false;
    private bool isIdle = false;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
    }

    void Update() {
        if (agent.desiredVelocity == Vector3.zero) {
            agent.SetDestination(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        } else {
            agent.SetDestination(player.transform.position);
        }

        float velocityMag = agent.desiredVelocity.magnitude;
        float velocityDiff = Vector3.Magnitude(agent.desiredVelocity - agent.velocity);

        if (velocityDiff <= runSpeedThreshold) {
            isRunning = true;
            isWalking = false;
        } else if (velocityMag <= idleSpeedThreshold) {
            isIdle = true;
            isRunning = false;
            isWalking = false;
        } else {
            isWalking = true;
            isRunning = false;
        }

        if (animator != null) {
            animator.SetBool("Running", isRunning);
            animator.SetBool("Walking", isWalking);
            animator.SetBool("Idle", isIdle);
        }
    }
}
