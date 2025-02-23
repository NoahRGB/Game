using UnityEngine;
using UnityEngine.AI;

public class EnemyPathfind : MonoBehaviour {

    private NavMeshAgent agent;
    private GameObject player;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
    }

    void FixedUpdate() {
        agent.SetDestination(player.transform.position);
    }

}
