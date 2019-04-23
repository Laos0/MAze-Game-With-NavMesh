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

    //public Camera camera;

    private bool doesPlayerHaveKey;
    private bool doesPlayerHaveVision;

	// Use this for initialization
	void Awake () {
        StartCoroutine(startGame());
    }

    // this method will be called through the FOVDetection script
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
        for(int i = 0; i < enemies.Capacity; i++)
        {
            enemies[i].GetComponent<Patrol>().isPlayerSpotted = true;
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
        GameObject e1 = createEnemy(new Vector3(89,2,89));
        e1.GetComponent<Patrol>().setRunSpd(10);
        e1.GetComponent<Patrol>().setWalkSpd(10);
 
        GameObject e2 = createEnemy(new Vector3(-32,2,117));
        GameObject e3 = createEnemy(new Vector3(-32,2,-49));
        GameObject e4 = createEnemy(new Vector3(64,2,16));
        e1.GetComponent<Patrol>().target = player;
        e2.GetComponent<Patrol>().target = player;
        e3.GetComponent<Patrol>().target = player;
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

        SceneManager.LoadScene(1);
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
        yield return new WaitForSeconds(1);
        configureNPC();
    }

    void configureNPC()
    {
        enemies[0].GetComponent<Patrol>().setRunSpd(10);
        enemies[0].GetComponent<Patrol>().setWalkSpd(10);
    }

    void destroyAllEnemies()
    {
        for(int i = 0; i < enemies.Capacity; i++)
        {
            Destroy(enemies[i]);
        }

        enemies = new List<GameObject>();
    }
}
