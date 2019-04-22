using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCPatrol : MonoBehaviour {

    // determines if the agent should wait on each patrol points
    [SerializeField]
    bool patrolWaiting;

    // the total time the npc waits at each patrol points
    [SerializeField]
    float totalWaitTime = 3f;

    // the probability of switching direction
    [SerializeField]
    float switchProbability;

    // the list of all patrol waypoints to visit
    [SerializeField]
    List<Waypoint> patrolPoints;

    // variables for base behavior 
    NavMeshAgent agent;
    int currentPatrolIndex;
    bool travelling;
    bool waiting;
    bool patrolForward;
    float waitTimer;



	// Use this for initialization
	public void Start () {
        agent = this.GetComponent<NavMeshAgent>();
        //agent.SetDestination(patrolPoints[0].transform.position);

        if (agent == null)
        {
            Debug.Log("The nav mesh agent component is not attaached to " + gameObject.name);
        }
        else
        {
            if(patrolPoints != null && patrolPoints.Count >= 2)
            {
                currentPatrolIndex = 0;
                setDestination();
            }
            else
            {
                Debug.Log("Insufficient patrol points");
            }
        }
	}
	
	// Update is called once per frame
	public void Update () {
		
        if(travelling && agent.remainingDistance <= 1.0f)
        {
            travelling = false;

            // if npc is going to wait, then wait
            if (patrolWaiting)
            {
                waiting = true;
                waitTimer = 0f;
            }
            else
            {
                changePatrolPoint();
                setDestination();
            }
        }

        // if we are waiting
        if (waiting)
        {
            waitTimer += Time.deltaTime;
            if(waitTimer >= totalWaitTime)
            {
                waiting = false;
                changePatrolPoint();
                setDestination();
            }
        }
	}

    private void setDestination()
    {
        if(patrolPoints != null)
        {
            Vector3 targetVec = patrolPoints[currentPatrolIndex].transform.position;
            agent.SetDestination(targetVec);
            travelling = true;
            Debug.Log("Setting Destination");
        }
    }

    private void changePatrolPoint()
    {
        Debug.Log("Changing position");
        if(UnityEngine.Random.Range(0f,1f) <= switchProbability)
        {
            patrolForward = !patrolForward;
        }

        if (patrolForward)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
        }
        else
        {
            if(--currentPatrolIndex < 0)
            {
                currentPatrolIndex = patrolPoints.Count - 1;
            }
        }
    }
}
