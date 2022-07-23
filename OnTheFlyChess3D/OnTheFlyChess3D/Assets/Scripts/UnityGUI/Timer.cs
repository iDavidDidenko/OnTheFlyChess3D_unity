using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 10;
    public static bool reset = false;
    public static bool startTimer = false;

    void Update()
    {
        if (startTimer)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                reset = true;
                Debug.LogError("Time has run out!");
                timeRemaining = 10;
                startTimer = false;
            }
        }
    }

}
