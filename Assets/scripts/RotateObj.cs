using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObj : MonoBehaviour {

    private float rotateSpd;
	// Use this for initialization
	void Start () {
        rotateSpd = 10f;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(15,30,45) * Time.deltaTime);
	}
}
