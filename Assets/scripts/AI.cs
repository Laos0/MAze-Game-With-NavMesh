using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour {

    public NavMeshAgent agent;
    public Transform target;
    public Transform locationTarget;

    public bool isPlayerSpotted;

    private List<Transform> positions;

    int randomPosition;
    

	// Use this for initialization
	void Start () {
        isPlayerSpotted = false;
        positions = new List<Transform>();
        goToRandomLocation();
    
    }
	
	// Update is called once per frame
	void Update () {
        // checking if the player has been seen
        isPlayerSpotted = gameObject.GetComponent<FOVDetection>().isInFOV;

        if(isPlayerSpotted == true)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            agent.SetDestination(locationTarget.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.tag == "position")
        {
            goToRandomLocation();
        }
    }

    // go to all possible locations except for the location that it is at
    // posToExclude is the AI's current hotspot location
    void goToRandomLocation(Transform posToExclude)
    {
        
    }

    // go to all available random hotspots 
    void goToRandomLocation()
    {
        GameObject[] locations = GameObject.FindGameObjectsWithTag("position");
        if(locations != null)
        {
            randomPosition = Random.Range(0, locations.Length);
            locationTarget = locations[randomPosition].transform;
            goToLocation(locationTarget.position);
            
        }
    }

    void goToLocation(Vector3 pos)
    {
        agent.SetDestination(pos);
    }

}
