using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Rendering.Universal;

public class CountUp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] TextMeshProUGUI countText;
    private bool countActive;
    private float currentTime;
    private int MINUTES_MAX = 1440;

    void Start()
    {
        ResetCount();
    }

    // This program is either counting or not counting
    // Update is called once per frame
    void Update()
    {
        if (countActive) //count
            UpdateCount();
        else //don't count
            StopCount();

        PrintCurrentTime();
    }

    // Reset the Count.
    // time = 0, counting isn't active, text color is black
    public void ResetCount()
    {
        countActive = false;
        currentTime = 0;
        countText.color = Color.black;
    }

    // Change countActive to Start or Stop Counting
    public void StartStopCount()
    {
        if (!countActive) //nothing is counting
            countActive = true;
        else //there is counting, stop
            StopCount();
    }

    // Private Functions
    //=========================================================================================

    // Count
    private void UpdateCount()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);

        if (countText.color != Color.black && minutes < MINUTES_MAX) //black means the timepiece is active or restarted
            countText.color = Color.black; 

        if (!countActive) //counting should be active
            countActive = true;
        else if (minutes < MINUTES_MAX) {
            currentTime += Time.deltaTime; //increase count
        }
        else { // count passed MINUTES_MAX or something went wrong
            countText.color = Color.red; 
        }
    }

    // Stop Count
    private void StopCount()
    {
        countActive = false;

        if (currentTime > 0) //faded grey color to show that it is paused
            countText.color = Color.grey;
    }

    private void PrintCurrentTime()
    {
        //TimeSpan time = TimeSpan.FromSeconds(currentTime);
        // Format & display the time
        //stopwatchText.text = string.Format("{0:00}:{1:00}", time.Minutes, time.Seconds);
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        countText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    //=========================================================================================
}
