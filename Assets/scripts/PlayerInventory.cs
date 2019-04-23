using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

    private bool isKeyItem;
    private bool isVisionItem;

    public Camera camera;
    public GameObject check;

    private List<GameObject> inActiveItems;

    int increaseView;

    private void Start()
    {
        isKeyItem = false;
        increaseView = 5;
        inActiveItems = new List<GameObject>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Condition to win, key must be obtained to open door
        if(collision.gameObject.tag == "key")
        {
            isKeyItem = true;
            check.SetActive(true);
            TheGameManager.Instance.GetComponent<TheGameManager>().setDoesPlayerHaveKey(isKeyItem);
            //Destroy(collision.gameObject);
            inActiveItems.Add(collision.gameObject);
            collision.gameObject.SetActive(false);

        }

        // increase the player's view
        if(collision.gameObject.tag == "visionItem")
        {
            isVisionItem = true;
            camera.GetComponent<CameraFollower>().offset.y += increaseView;
            TheGameManager.Instance.GetComponent<TheGameManager>().setDoesPlayerHaveVision(isVisionItem);
            //Destroy(collision.gameObject);
            inActiveItems.Add(collision.gameObject);
            collision.gameObject.SetActive(false);
        }

        if(collision.gameObject.tag == "speedItem")
        {
            //Debug.Log("SPEED BOOST");
            TheGameManager.Instance.GetComponent<TheGameManager>().increasePlayerSpeed();
            inActiveItems.Add(collision.gameObject);
            //Destroy(collision.gameObject);
            collision.gameObject.SetActive(false);
        }


        // need key to open the door
        if(collision.gameObject.tag == "door")
        {
            if(isKeyItem != false)
            {
                TheGameManager.Instance.GetComponent<TheGameManager>().winGame();
            }
        }
    }
}
