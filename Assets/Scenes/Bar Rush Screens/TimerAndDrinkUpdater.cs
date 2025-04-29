using UnityEngine;
using TMPro;

public class TimerAndDrinkUpdater : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI drinkCountText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (TimerManager.Instance != null)
        {
            TimerManager.Instance.StartTimer(GameSettings.selectedTime); // Start 5-minute timer
        }
        else
        {
            Debug.LogError("TimerManager.Instance is null! Is TimerObject in the scene?");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerManager.Instance != null)
        {
            float t = TimerManager.Instance.timeRemaining;
            // Debug.Log($"Current time: {t}");
            int minutes = Mathf.FloorToInt(t / 60);
            int seconds = Mathf.FloorToInt(t % 60);
            timerText.text = $"Timer: {minutes:00}:{seconds:00}";
        }

        drinkCountText.text = $"Drinks: {GameStats.DrinkServed}";
    }
}
