using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public TextMeshProUGUI Timer;
    public int MaxMinutesGiven = 1;
    public int MaxSecondsGiven = 20;
    protected float TotalStartingTime;
    

    protected float CurrentTime;
    protected bool CountDownStarted = false;

    private void Start()
    {
        Timer = GameObject.Find("Time Remaining").GetComponent<TextMeshProUGUI>();
        ConvertToSeconds();
        EventHandler.EventHandlerInstance.CheckpointTriggered.AddListener(Addtime);

        CurrentTime = TotalStartingTime;
        if( CurrentTime > 0)
        {
            CountDownStarted = true;
        }

        
    }

    public void Addtime()
    {
        CurrentTime += 10;
    }

    public void Update()
    {
        if (CountDownStarted)
        {
            // Update the timer as float ticks down
            CurrentTime -= Time.deltaTime;

            if (CurrentTime <= 0)
            {
                EventHandler.EventHandlerInstance.RaceFailed.Invoke();
                CountDownStarted = false;
            }
            UpdateTime();
        }
    }

    //Converts all time into seconds to count down easier
    public void ConvertToSeconds()
    {
        TotalStartingTime = (MaxMinutesGiven * 60) + MaxSecondsGiven;
    }

    //Updates how much time is remaining in a readable format using string formatting by utalizing thse curly bracey 
    public void UpdateTime()
    {
        
        int minutes = Mathf.FloorToInt(CurrentTime / 60);
        int seconds = Mathf.FloorToInt(CurrentTime % 60);

        
        Timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
