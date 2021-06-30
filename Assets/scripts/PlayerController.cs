﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerController : MonoBehaviour {

    public Camera camera;

    public NavMeshAgent agent;

    public ThirdPersonCharacter character;

    public GameObject clickEffect;

    private void Start()
    {
        agent.updateRotation = false;
    }

    // Update is called once per frame
    void Update () {

        // if the left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            //Instantiate(clickEffect, transform.position, Quaternion.identity);
            // Take mouse position in screen coordinate and convert it to a ray 
            // that shoots out in the direction we click
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


            if(Physics.Raycast(ray, out hit)) // shooting out another ray to see what we hit
            {
  
                agent.SetDestination(hit.point);
                Vector3 newPoint = new Vector3(hit.point.x, .3f, hit.point.z);
                clickEffect.transform.position = newPoint;
                //clickEffect.transform.position.Set(0, 0, 0);
                // Move player/agent

            }
        }

        if(agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity, false, false);
        }
        else
        {
            character.Move(Vector3.zero, false, false);
        }
	}
}
