using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerCountDown : MonoBehaviour {

    private int duration;
    private bool isInProcess;

    void Start()
    {
        duration = 0;
        isInProcess = false;

    }

    public void startCoutDown()
    {
        if (isInProcess == false)
        {
            isInProcess = true;
            StartCoroutine(countDown());

        }
    }

    public void setDuration(int durationInt)
    {
        duration = durationInt;
    }

    IEnumerator countDown()
    {
        yield return new WaitForSeconds(1);
        duration--;
        if(duration > 0)
        {
            StartCoroutine(countDown());
        }
        else
        {
            isInProcess = false;
        }
    }
}
