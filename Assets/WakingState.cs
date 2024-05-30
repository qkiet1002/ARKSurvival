using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WakingState : StateMachineBehaviour
{
    float timer;
    List<Transform> wayPoints = new List<Transform>();
    NavMeshAgent agent;

    Transform player;

    float ChaseRange = 10;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isRuning", false);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = 3.5f;
        timer = 0;
        GameObject go = GameObject.FindGameObjectWithTag("WP");
        foreach (Transform t in go.transform)
            wayPoints.Add(t);
        agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Count)].position);
        Debug.Log("Entered Walking State");

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.remainingDistance < agent.stoppingDistance)
            agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Count)].position);

        timer += Time.deltaTime;
        if (timer > 10)
        {
            animator.SetBool("isWalking", false);
            Debug.Log("Setting isWalking to false");
        }
        float distance = Vector3.Distance(player.position, animator.transform.position);
        Debug.Log($"Patrol State - Distance to player: {distance}");
        if (distance < ChaseRange)
        {
            Debug.Log("Player is within chase range. Setting isRuning to true.");
            animator.SetBool("isRuning", true);
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);

        Debug.Log("Exiting Patrol State");
    }
}
