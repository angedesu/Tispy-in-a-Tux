using TMPro;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        alcoholStatsText.text = $"✔ {GameStats.AlcoholCorrect}   ❌ {GameStats.AlcoholWrong}";
        mixerStatsText.text   = $"✔ {GameStats.MixerCorrect}   ❌ {GameStats.MixerWrong}";
        garnishStatsText.text = $"✔ {GameStats.GarnishCorrect}   ❌ {GameStats.GarnishWrong}";
        int drinksMade = 10 - GameStats.DrinksRemaining;
        drinksMadeText.text = $"Drinks Made: {drinksMade}";

        float time = TimerManager.Instance?.timeRemaining ?? 0;
        if (time <= 0)
            timeLeftText.text = "Ran Out of Time";
        else
        {
            int min = Mathf.FloorToInt(time / 60);
            int sec = Mathf.FloorToInt(time % 60);
            timeLeftText.text = $"Time Left:\n{min:00}:{sec:00}";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public TextMeshProUGUI alcoholStatsText;
    public TextMeshProUGUI mixerStatsText;
    public TextMeshProUGUI garnishStatsText;
    public TextMeshProUGUI drinksMadeText;
    public TextMeshProUGUI timeLeftText;
}
