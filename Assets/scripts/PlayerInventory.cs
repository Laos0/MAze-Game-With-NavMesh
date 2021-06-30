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
    public GameObject wayOutToggle;
    public GameObject wayOutCheck;

    public GameObject light;

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
        // DONT WORK: collision.gameObject.GetComponent<Item>().itemType == ItemType.KEY
        // original: item.itemType == ItemType.KEY

        if(item != null)
        {

            if (item.itemType == ItemType.KEY)
            {
                isKeyItem = true;
                check.SetActive(true);
                TheGameManager.Instance.setDoesPlayerHaveKey(isKeyItem);
                inActiveItems.Add(collision.gameObject);
                collision.gameObject.SetActive(false);
                if(isKeyItem == true)
                {
                    wayOutToggle.SetActive(true);
                }

            }

            //collision.gameObject.GetComponent<Item>().

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
   
                light.GetComponent<Light>().color = new Color(0f/255f, 255f/255f, 184f/225f);
                //FFFBAC -yellow
                inActiveItems.Add(collision.gameObject);
                collision.gameObject.SetActive(false);
                startCoutDown();
            }
        }

        // need key to open the door
        if (collision.gameObject.tag == "door")
        {
            if (isKeyItem != false)
            {
                wayOutCheck.SetActive(true);
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
            light.GetComponent<Light>().color = new Color(255f/255f, 251/255f, 172/255f);
            // when duration reaches 0
            isInProcess = false;
            speedBoostTxt.SetActive(false);
            duration = 30;
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
