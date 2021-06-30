using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            Debug.Log("GAME OVER");
            TheGameManager.Instance.GetComponent<TheGameManager>().gameOver();
        }
    }

}
