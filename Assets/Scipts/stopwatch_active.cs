using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class stopwatch_active : MonoBehaviour
{
    private bool stopwatchActive;
    private float currentTime;
    [SerializeField] TextMeshProUGUI stopwatchText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stopwatchActive = false;
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (stopwatchActive == true) {
            currentTime = currentTime + Time.deltaTime;
            if (currentTime >= 86400) {
                stopwatchActive = false;
                Start();
                Debug.Log("Stopwatch Maximum Time (24 hours) Reached!");
            }
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);

        // Format & display the time
        stopwatchText.text = string.Format("{0:00}:{1:00}", time.Minutes, time.Seconds);
    }

    public void StartStopStopwatch() {
        if (stopwatchActive) // stop watch is active, stop stopwatch
        {
            stopwatchActive = false;
        }
        else { // stop watch is not acive, start stopwatch
            stopwatchActive = true;
        }
    }

    public void ResetStopwatch() {
        Start();
        Debug.Log("Stopwatch is reset!");
    }

    /*
    public void StartStopwatch() {
        stopwatchActive = true;
    }

    public void StopStopwatch() {
        stopwatchActive = false;
    }*/
}
