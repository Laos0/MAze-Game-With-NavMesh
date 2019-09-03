using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class TheGameManager : Singleton<TheGameManager> {

    public bool isPlayerSpotted; // checks if the player is detected
   
    public GameObject prefab; // enemy prefab to be instantiated
    public GameObject player;
    public List<GameObject> enemies;

    public GameObject gameOverMenuUI;
    public GameObject winGameUI;

    public Transform playerStartingPos;


    //public Camera camera;

    private bool doesPlayerHaveKey;
    private bool doesPlayerHaveVision;

	// Use this for initialization
	void Awake () {
        // wait til all the npc are created
        StartCoroutine(startGame());
    }

    // this method will be called through the FOVDetection script
    public void setIsPlayerSpotted(bool trueOrFalse)
    {
        isPlayerSpotted = trueOrFalse;
        if(isPlayerSpotted == true)
        {
            Debug.Log("PLAYER HIT!");
            alertNPC();
        }
    }

    public void alertNPC()
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            enemies[i].GetComponent<Patrol>().alertNPC = true;
        }
    }

    private GameObject createEnemy(Vector3 vec)
    {
        if(prefab != null && vec != null)
        {
            GameObject enemy = Instantiate(prefab, vec, Quaternion.identity);

            if(player != null)
            {
                enemy.GetComponent<FOVDetection>().playerTransformation = player.transform;
                enemies.Add(enemy);
                return enemy;
            }

        }

        return null;
    }

    private void populateEnemies()
    {
        GameObject e1 = createEnemy(new Vector3(89, 2, 89));
        GameObject e2 = createEnemy(new Vector3(-32,2,117));
        GameObject e3 = createEnemy(new Vector3(-32,2,-49));
        GameObject e4 = createEnemy(new Vector3(64,2,16));
        GameObject e5 = createEnemy(new Vector3(29, 2, 29));
    }

    public void gameOver()
    {
        gameOverMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void exitGame()
    {
        Application.Quit();
    }

    public void restartGame()
    {
        destroyAllEnemies();
        player.GetComponent<PlayerInventory>().resetInActiveObj();
        resetPlayerPosition();
        gameOverMenuUI.SetActive(false);
        winGameUI.SetActive(false);
        Time.timeScale = 1f;
        StartCoroutine(startGame());
    }

    public void setDoesPlayerHaveKey(bool haveKey)
    {
        doesPlayerHaveKey = haveKey;
    }

    public void winGame()
    {
        winGameUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void setDoesPlayerHaveVision(bool haveVision)
    {
        doesPlayerHaveVision = haveVision;
    }

    public void increasePlayerSpeed()
    {
        player.GetComponent<NavMeshAgent>().speed = 5;
        startTimer();
    }

    public void resetPlayerSpeed()
    {
        player.GetComponent<NavMeshAgent>().speed = 2;
    }

    public void startTimer()
    {
        // For the speed buff
    }

    IEnumerator startGame()
    {
        enemies = new List<GameObject>();
        isPlayerSpotted = false;
        populateEnemies();
        doesPlayerHaveKey = false;
        yield return new WaitForSeconds(2);
        configureNPC();
    }

    void configureNPC()
    {
        enemies[0].GetComponent<Patrol>().setRunSpd(3f);
        enemies[0].GetComponent<Patrol>().setWalkSpd(3f);

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].GetComponent<Patrol>().target = player;
        }
    }

    void destroyAllEnemies()
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            Destroy(enemies[i]);
        }

        enemies = new List<GameObject>();
    }

    void resetPlayerPosition()
    {
        for(int i = 0; i < 5; i++)
        {
            player.transform.position = playerStartingPos.position;

        }
    }
}
