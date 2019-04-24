using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class PlayerInventory : MonoBehaviour {

    private bool isKeyItem;
    private bool isVisionItem;

    private int duration;
    private bool isInProcess;

    public Camera camera;
    public GameObject check;

    public GameObject speedBoostTxt;
    public Text durationTxt;

    private List<GameObject> inActiveItems;

    int increaseView;

    private void Start()
    {
        isKeyItem = false;
        increaseView = 5;
        inActiveItems = new List<GameObject>();

        duration = 30;
        isInProcess = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Item item = collision.gameObject.GetComponent<Item>();
        // Condition to win, key must be obtained to open door
        if(item.itemType == ItemType.KEY)
        {
            isKeyItem = true;
            check.SetActive(true);
            TheGameManager.Instance.setDoesPlayerHaveKey(isKeyItem);
            inActiveItems.Add(collision.gameObject);
            collision.gameObject.SetActive(false);

        }

        // increase the player's view
        if(item.itemType == ItemType.EAGLE_VIEW)
        {
            isVisionItem = true;
            camera.GetComponent<CameraFollower>().offset.y += increaseView;
            TheGameManager.Instance.setDoesPlayerHaveVision(isVisionItem);
            inActiveItems.Add(collision.gameObject);
            collision.gameObject.SetActive(false);
        }

        if(item.itemType == ItemType.SPEED_BOOST)
        {
            //Debug.Log("SPEED BOOST");
            TheGameManager.Instance.increasePlayerSpeed();
            speedBoostTxt.SetActive(true);
            inActiveItems.Add(collision.gameObject);
            collision.gameObject.SetActive(false);
            startCoutDown();
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

    public void resetInActiveObj()
    {
        if(inActiveItems != null)
        {
           for(int i = 0; i < inActiveItems.Count; i++)
            {
                inActiveItems[i].SetActive(true);
            }
        }

        inActiveItems = new List<GameObject>();
    }

    IEnumerator countDown()
    {
        yield return new WaitForSeconds(1);
        duration--;
        durationTxt.text = duration.ToString();
        if (duration > 0)
        {
            StartCoroutine(countDown());
        }
        else
        {
            // when duration reaches 0
            isInProcess = false;
            speedBoostTxt.SetActive(false);
            gameObject.GetComponent<NavMeshAgent>().speed = 3;
        }
    }

    public void startCoutDown()
    {
        if (isInProcess == false)
        {
            isInProcess = true;
            StartCoroutine(countDown());

        }
        else
        {
            duration += 30;
        }
    }

}
