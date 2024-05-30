using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idleState : StateMachineBehaviour
{
    float timer;
    public List<Transform> players = new List<Transform>();
    float ChaseRange = 10f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isRunning", false);
        timer = 0f;

        // Find all player objects and add their transforms to the list
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerObject in playerObjects)
        {
            players.Add(playerObject.transform);
        }

        Debug.Log("Entered Idle State");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if (timer > 5f)
        {
            animator.SetBool("isWalking", true);
            Debug.Log("Setting isWalking to true");
        }

        // Check the distance to each player
        foreach (Transform player in players)
        {
            float distance = Vector3.Distance(player.position, animator.transform.position);
            Debug.Log($"Idle State - Distance to player: {distance}");

            if (distance < ChaseRange)
            {
                Debug.Log("Player is within chase range. Setting isRunning to true.");
                animator.SetBool("isRunning", true);
                break; // Exit the loop as we only need one player within range to start running
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Exiting Idle State");
    }
}
