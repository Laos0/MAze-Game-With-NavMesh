using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionControl : MonoBehaviour {

    public Material mat;

	// Use this for initialization
	void Start () {
        mat.EnableKeyword("_EMISSION");
	}

}
