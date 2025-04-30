using UnityEngine;

public class GameStats : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static int DrinksRemaining = 10;
    public static int AlcoholCorrect = 0;
    public static int AlcoholWrong = 0;
    public static int MixerCorrect = 0;
    public static int MixerWrong = 0;
    public static int GarnishCorrect = 0;
    public static int GarnishWrong = 0;

    // use this function when "serving" a drink
    public static void ServedDrink()
    {
        DrinksRemaining--;
    }

    // everytime a game starts
    public static void ResetStats()
    {
        DrinksRemaining = 10;
        AlcoholCorrect = 0;
        AlcoholWrong = 0;
        MixerCorrect = 0;
        MixerWrong = 0;
        GarnishCorrect = 0;
        GarnishWrong = 0;
    }
}
