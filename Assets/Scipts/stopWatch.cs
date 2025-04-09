using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class stopWatch : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI stopwatchText;
    private float elapsedTime;
    private bool isRunning = false;
    private bool isPaused = false;


    // Update is called once per frame
    void Update()
    {

        if (isRunning && !isPaused)
        {
            UpdateTime();
        }
    }

    // Start or Resume the Timer
    public void StartStopWatch()
    {
        if (!isRunning)
        {
            isRunning = true;
        }
        isPaused = false;
    }

    // Pause the Timer
    public void PauseStopWatch()
    {
        if (isRunning)
        {
            isPaused = true;
            isRunning = false;
        }
    }

    // Reset the Timer
    public void ResetStopWatch()
    {
        elapsedTime = 0;
        isRunning = false;
        isPaused = true;
        UpdateTime();
    }

    public void UpdateTime()
    {
        elapsedTime += Time.deltaTime; // increase by elapsed time by 1 every second
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        // Format & display the time
        stopwatchText.text = string.Format("{00:00}:{01:00}", minutes, seconds);
    }

}