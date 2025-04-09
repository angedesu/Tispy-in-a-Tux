using UnityEngine;
using TMPro;

public class timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    private float elapsedTime;
    private bool isRunning = false;
    private bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        /*
        elapsedTime += Time.deltaTime; // increase by elapsed time by 1 every second
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        */

        if (isRunning && !isPaused) {
            UpdateTime();
        }
    }

    // Start or Resume the Timer
    public void StartTimer() {
        if (!isRunning)
        {
            isRunning = true;
        }
        isPaused = false;
    }

    // Pause the Timer
    public void PauseTimer() {
        if (isRunning) {
            isPaused = true;
            isRunning = false;
        }
    }

    // Reset the Timer
    public void ResetTimer() {
        elapsedTime = 0;
        isRunning = false;
        isPaused = true;
        UpdateTime();
    }

    public void UpdateTime() {
        elapsedTime += Time.deltaTime; // increase by elapsed time by 1 every second
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        // Format & display the time
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}