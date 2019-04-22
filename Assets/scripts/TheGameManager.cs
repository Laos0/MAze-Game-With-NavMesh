using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheGameManager : Singleton<TheGameManager> {

    public bool isPlayerSpotted;
    public GameObject prefab;
    public GameObject player;
    public List<GameObject> enemies;

	// Use this for initialization
	void Start () {
        enemies = new List<GameObject>();
        isPlayerSpotted = false;
        createEnemy(new Vector3(1,1,1));
    }
	
	// Update is called once per frame
	void Update () {
		if(isPlayerSpotted == true)
        {
            Debug.Log("IsPlayerSpotted has been updated");
            
        }
	}

    public void setIsPlayerSpotted(bool trueOrFalse)
    {
        isPlayerSpotted = trueOrFalse;
        if(isPlayerSpotted == true)
        {
            alertNPC();
        }
    }

    public void alertNPC()
    {
        
    }

    private void createEnemy(Vector3 vec)
    {
        if(prefab != null && vec != null)
        {
            GameObject enenmy = Instantiate(prefab, vec, Quaternion.identity);

            enenmy.GetComponent<FOVDetection>().playerTransformation = player.transform;

            enemies.Add(enenmy);
        }
    }

    private void populateEnemies()
    {
        createEnemy(new Vector3());
        createEnemy(new Vector3());
        createEnemy(new Vector3());
        createEnemy(new Vector3());
    }
    
}
