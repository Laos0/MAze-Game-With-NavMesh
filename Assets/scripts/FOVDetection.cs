using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVDetection : MonoBehaviour {

    public Transform playerTransformation;
    public GameObject AI;
    public float maxAngle;
    public float maxRadius;

    public bool isInFOV;

    private void Update()
    {
        isInFOV = inFOV(transform, playerTransformation, maxAngle, maxRadius);
    }

    /*The one is for visio purposes of the gizmos
     * */
    private void OnDrawGizmos()
    {
        if(playerTransformation != null) {
            // draw a radius around the AI
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, maxRadius);

            // defining how the lines will be drawn
            Vector3 fovLine1 = Quaternion.AngleAxis(maxAngle, transform.up) * transform.forward * maxRadius;
            Vector3 fovLine2 = Quaternion.AngleAxis(-maxAngle, transform.up) * transform.forward * maxRadius;

            // drawing the lines of the defined lines
            // these two lines combined will be our AI's field of view
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, fovLine1);
            Gizmos.DrawRay(transform.position, fovLine2);

            // The green line will always point towards the player
            if (!isInFOV)
            {
                Gizmos.color = Color.green;
            }
            else
            {
                // Player is detected
                Gizmos.color = Color.red;
            }
            Gizmos.DrawRay(transform.position, (playerTransformation.position - transform.position).normalized * maxRadius);

            // the black line will always be foward
            /*Gizmos.color = Color.black;
            Transform targetLoc = gameObject.GetComponent<AI>().locationTarget;
            if (targetLoc != null)
            {
                Gizmos.DrawLine(transform.position, targetLoc.forward * maxRadius);
                // original: Gizmos.DrawLine(transform.position, transform.forward * maxRadius);
            }
            */
        }
        

    }

    // allow us to call this script in the entire game
    public static bool inFOV(Transform checkingObj, Transform target, float maxAngle, float maxRadius)
    {

        Collider[] overlaps = new Collider[100];
        // The checkingObj is the AI
        // checks every objects in the radius
        // Then every objects checked will be passed to the overlaps array
        // the int count is how many overlaps there were
        int count = Physics.OverlapSphereNonAlloc(checkingObj.position, maxRadius, overlaps);

        for(int i = 0; i < count + 1; i++)
        {

            if(overlaps[i] != null)
            {
                if(overlaps[i].transform == target)
                {
                    Vector3 directionBetween = (target.position - checkingObj.position).normalized;
                    directionBetween.y *= 0; // making sure the direction of the y is always 0, so height is not a factor in the angle

                    // red line is the direction between and black line is forward 
                    float angle = Vector3.Angle(checkingObj.forward, directionBetween);

                    // if we are within the angle t
                    if(angle <= maxAngle)
                    {
                        Ray ray = new Ray(checkingObj.position, target.position - checkingObj.position);
                        RaycastHit hit;

                        if(Physics.Raycast(ray, out hit, maxRadius))
                        {
                            if(hit.transform == target)
                            {
                                // Updating the GameManager's isPLayerSpotted variable
                                TheGameManager.Instance.GetComponent<TheGameManager>().setIsPlayerSpotted(true);
                                return true;
                            }
                        }
                    }
                }
            }
        }

        return false;
    }
}
