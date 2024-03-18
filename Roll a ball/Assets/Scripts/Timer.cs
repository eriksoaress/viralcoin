using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeRemaining;
    public bool timerIsRunning;
    public TMP_Text timeText;
    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = 60;
        timerIsRunning = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
            }	
        }
        
    }

    void DisplayTime(float timeToDisplay)
    {
    if (timeToDisplay > 0)
    {     
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float decimalseconds = Mathf.FloorToInt((timeToDisplay * 100) % 100);
        timeText.text = string.Format("{0}:{1:00}", seconds, decimalseconds);
    }else{
        timeText.text = "0:00";
    }    
    }
}
