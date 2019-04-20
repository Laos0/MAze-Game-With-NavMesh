using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {

    public Camera camera;

    public NavMeshAgent agent;

    // Update is called once per frame
    void Update () {

        // if the left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Take mouse position in screen coordinate and convert it to a ray 
            // that shoots out in the direction we click
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


            if(Physics.Raycast(ray, out hit)) // shooting out another ray to see what we hit
            {
                // Move player/agent
                agent.SetDestination(hit.point);
            }
        }
	}
}
