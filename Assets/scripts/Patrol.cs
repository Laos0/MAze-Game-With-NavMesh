using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    private int destPoint = 0;
    private NavMeshAgent agent;

    public bool isPlayerSpotted;
    public bool alertNPC;

    public GameObject target;
    GameObject[] locations;

    private int randomPosition;

    float walkSpd, runSpd, targetSpd;


    void Start()
    {
        locations = GameObject.FindGameObjectsWithTag("patrolPoint");
        isPlayerSpotted = false;
        agent = GetComponent<NavMeshAgent>();
        setWalkSpd(.5f);
        setRunSpd(1f);


        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        GotoNextPoint();
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        // original: points
        if (locations.Length == 0)
        {
            return;
        }

        // Set the agent to go to the currently selected destination.
        randomPosition = Random.Range(0, locations.Length);
        agent.destination = locations[randomPosition].transform.position;
    }

    void Update()
    {
        // Checking to see if the player is within the field of view
        isPlayerSpotted = gameObject.GetComponent<FOVDetection>().isInFOV;
            
            if (isPlayerSpotted)
            {
                agent.SetDestination(target.transform.position);
                targetSpd = runSpd;
                changeSpd();

            }
            else
            {
                targetSpd = walkSpd;
                changeSpd();
                Debug.Log("PLAYER IS NOT SPOTTED");
                // Choose the next destination point when the agent gets
                // close to the current one.
                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                {
                    GotoNextPoint();
                }
            }
        
    
    }

    void changeSpd()
    {
        agent.GetComponent<NavMeshAgent>().speed = targetSpd;
        Debug.Log("speed change");
    }

    public void setRunSpd(float num)
    {
        runSpd = num;
    }

    public void setWalkSpd(float num)
    {
        walkSpd = num;
    }

}
