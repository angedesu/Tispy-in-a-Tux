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

    public static int DrinkServed = 10;

    // use this function when "serving" a drink
    public static void ServedDrink()
    {
        DrinkServed--;
    }

    // everytime a game starts
    public static void ResetStats()
    {
        DrinkServed = 10;
    }
}
