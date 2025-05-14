using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MenuMover : MonoBehaviour {

    public float xDistanceToMove;
    public bool isRunner = false;
    private NavMeshAgent agent;
    private Animator animator;

    private Vector3 resetPoint;
    private Vector3 target;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        animator.SetBool("Running", isRunner);
        animator.SetBool("Walking", !isRunner);

        resetPoint = transform.localPosition;
        target = new Vector3(resetPoint.x + xDistanceToMove, resetPoint.y, resetPoint.z);
    }

    void Update() {
        agent.SetDestination(target);

        if (agent.remainingDistance <= 1.0f) {
            transform.localPosition = resetPoint;
        }
    }
}
