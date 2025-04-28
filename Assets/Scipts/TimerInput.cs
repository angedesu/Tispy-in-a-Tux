using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerInput : MonoBehaviour
{
    public TMP_InputField timeInputField;
    public TMP_Text countdownText;

    private float countdownTime = 0;
    private bool countdownActive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (countdownActive)
            UpdateCount();

        //PrintCountdownTime();
    }

    public void ResetTimer()
    {
        countdownActive = false;
        countdownText.color = Color.black;
        countdownTime = 0;
        timeInputField.text = "";
        countdownText.text = "00:00";
    }

    public void StartStopTimer()
    {
        if (!countdownActive)
        {
            if (countdownTime == 0) // Only set timer if countdownTime is zero
            {
                if (SetTimer()) // Only start if the timer is set successfully
                    countdownActive = true;
                else
                    return; // Invalid input, don't start
            }
            else if (countdownTime > 0)
                countdownActive = true;
        }
        else
        {
            StopCount();
        }
    }

    public void TryStartTimer()
    {
        if (SetTimer())
            StartTimer();
    }

    private void StartTimer()
    {
        countdownActive = true;
        PrintCountdownTime();
    }

    //============================================================================================
    private void PrintCountdownTime()
    {
        int minutes = Mathf.FloorToInt(countdownTime / 60);
        int seconds = Mathf.FloorToInt(countdownTime % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public bool SetTimer()
    {
        string input = timeInputField.text;

        if (string.IsNullOrEmpty(input) || input == "__:__")
        {
            input = "06:00"; // Default to 6 minutes if no input
            timeInputField.text = input; // Update visible text
        }

        if (ParseTime(input, out int minutes, out int seconds))
        {
            countdownTime = minutes * 60 + seconds;
            countdownText.color = Color.black; // Reset color to black
            PrintCountdownTime(); // <<< ADD THIS LINE
            return true;
        }
        else
        {
            countdownText.color = Color.red; // Display error in red
            countdownText.text = "Invalid Input!";
            return false;
        }
    }


    private void UpdateCount()
    {
        if (countdownActive)
        {
            countdownTime -= Time.deltaTime;
            Debug.Log("Countdown time: " + countdownTime); // << Add this temporarily

            if (countdownTime <= 0)
            {
                countdownTime = 0;
                countdownActive = false;
                countdownText.color = Color.red;
                countdownText.text = "Time's Up!";
            }
            else
            {
                countdownText.color = Color.black; // Keep the color black while counting down
                PrintCountdownTime();
            }
        }
    }


    private void StopCount() {
        countdownActive = false;

        if (countdownTime > 0) //faded grey color to show that it is paused
            countdownText.color = Color.grey;
    }

    bool ParseTime(string time, out int minutes, out int seconds)
    {
        minutes = 0;
        seconds = 0;

        string[] timeParts = time.Split(':');
        if (timeParts.Length == 2)
        {
            if (int.TryParse(timeParts[0], out minutes) && int.TryParse(timeParts[1], out seconds))
                return true;
        }

        return false;
    }

}
