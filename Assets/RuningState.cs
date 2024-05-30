using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class RuningState : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;
    float ChaseRange = 10;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // animator.SetBool("isChasing", false);
        agent = animator.GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        agent.speed = 6f;
        Debug.Log("Entered Runing State");

    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(player.position);

        float distance = Vector3.Distance(player.position, animator.transform.position);
        if (distance > ChaseRange)
        {
            Debug.Log("Player is within chase range. Setting isChasing to false.");
            animator.SetBool("isRuning", false);
        }
        if (distance < 3f)
        {
            Debug.Log("Player is within chase range. Setting isAttacking to true.");
            animator.SetBool("isAtacking", true);
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(animator.transform.position);
        Debug.Log("Exiting Chase State");
    }
}
