using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Rendering.Universal;

public class countUpDown : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool countingUp = true; // stopwatch counts up
    [SerializeField] TextMeshProUGUI timeText;
    public Image playPauseBtn;
    private bool stopwatchActive;
    private bool timerActive;
    private float currentTime;
    private bool isRestarted;
    //[SerializeField] TextMeshProUGUI stopwatchText;
    //[SerializeField] TextMeshProUGUI timerText;

    void Start()
    {
        ResetCount();
    }

    // Update is called once per frame
    void Update()
    {
        if (stopwatchActive || timerActive)
            UpdateCount();
        else
            StopCount();

        PrintCurrentTime();
    }

    public void ResetCount()
    {
        stopwatchActive = false;
        timerActive = false;
        currentTime = 0;
        timeText.color = Color.black;
        isRestarted = true;
    }

    public void StartStopCount() {
        if (!stopwatchActive && !timerActive) //nothing is counting
        {
            if (countingUp)
                stopwatchActive = true;
            else
                timerActive = true;
        }
        else //don't count
            StopCount();
    }

    public void UpdateCount() {
        timeText.color = Color.black; //black means the timepiece is active or restarted

        UpdateStopwatch();
        UpdateTimer();

        /*
        if (countingUp)
            UpdateStopwatch();
        else if (!countingUp)
            UpdateTimer();
        else
            StopCount();
        */
    }
    public void StopCount()
    {
        if (countingUp && stopwatchActive)
            stopwatchActive = false;
        else if (!countingUp && timerActive)
            timerActive = false;
        else
        {
            stopwatchActive = false;
            timerActive = false;
        }

        if (currentTime > 0) //faded grey color to show that it is paused
            timeText.color = Color.grey;
    }

    //============================================================================================
    private void PrintCurrentTime()
    {
        //TimeSpan time = TimeSpan.FromSeconds(currentTime);
        // Format & display the time
        //stopwatchText.text = string.Format("{0:00}:{1:00}", time.Minutes, time.Seconds);
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void UpdateStopwatch()
    {
        if (countingUp) //it's counting up, as a stopwatch does
        {
            if (!stopwatchActive) //stopwatch should be active
                stopwatchActive = true;
            else
                currentTime += Time.deltaTime;

        }
        else if (stopwatchActive) // Don't count up, stopwatch should be inactive
            stopwatchActive = false;

        //else don't update & do nothing
    }

    private void UpdateTimer() {
        if (!countingUp) //counting down, as a timer does
        {
            if (!timerActive) //timer should be active
                timerActive = true;
            else
            {
                if (currentTime > 0) // countdown till 0
                { 
                    currentTime -= Time.deltaTime;
                    isRestarted = false;
                }
                else if (currentTime < 0) // if countdown goes below 0, reset to 0
                    currentTime = 0;
                //else if (currentTime == 0) // time's up! 
                   //timeText.color = Color.red;
            }
        }
        else if (timerActive) // Don't count down, timer should be inactive
            timerActive = false;

        //else don't update & do nothing
    }
    //============================================================================================


    public void SwitchCountUporDown() {
        countingUp = !countingUp;
        ResetCount();

        var pos = timeText.rectTransform.localPosition;
        var pos_image = playPauseBtn;
        if (countingUp)
        {
            timeText.rectTransform.localPosition = new Vector2(pos.x, 0);
            timeText.rectTransform.anchoredPosition = new Vector2(pos.x, 0);
            playPauseBtn.rectTransform.localPosition = new Vector2(pos.x, -174);
            playPauseBtn.rectTransform.anchoredPosition = new Vector2(pos.x, -174);
        }
        else
        {
            timeText.rectTransform.localPosition = new Vector2(pos.x, 100);
            timeText.rectTransform.anchoredPosition = new Vector2(pos.x, 100);
            playPauseBtn.rectTransform.localPosition = new Vector2(pos.x, -74);
            playPauseBtn.rectTransform.anchoredPosition = new Vector2(pos.x, -74);
        }
    }
}
