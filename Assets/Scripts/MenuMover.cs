using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MenuMover : MonoBehaviour {

    // a basic enemy that just moves a specified distance in the x direction and then
    // resets itself back to the start
    // used in the main menu / game over menu

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

        // reset back to the start
        if (agent.remainingDistance <= 1.0f) {
            transform.localPosition = resetPoint;
        }
    }
}
