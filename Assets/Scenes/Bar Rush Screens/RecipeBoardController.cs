using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class RecipeBoardController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShowRandomDrink();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI ingredientsText;
    public DrinkDataManager dataManager;

    private readonly List<string> drinkNames = new List<string>
    {
        "Screwdriver", "Americano", "Rusty Nail", "Stinger", "Negroni",
        "Old Fashioned", "Dry Martini", "Manhattan", "Whiskey Sour", "Daiquiri"
    };

    private void ShowRandomDrink()
    {
        int index = Random.Range(0, drinkNames.Count);
        string randomDrink = drinkNames[index];
        ShowDrink(randomDrink);
    }

    private void ShowDrink(string drinkName)
    {
        StartCoroutine(dataManager.GetDrinkData(drinkName, OnDrinkDataReceived));
    }
    
    private void OnDrinkDataReceived(DrinkRecipeData data)
    {
        if (data == null)
        {
            titleText.text = "No Drink found.";
            ingredientsText.text = "";
            return;
        }

        // drink title
        titleText.text = data.title;

        // custom instruction display
        string output = "";

        // start with ice
        output += "-----------ICE-----------\n";

        // alcohols
        foreach (string alcohol in data.alcohols)
        {
            output += $"{alcohol}\n";
        }

        // mixers
        foreach (string mixer in data.mixers)
        {
            output += $"{mixer}\n";
        }

        // mix step
        output += "----------SHAKE----------\n";

        // garnishes
        foreach (string garnish in data.garnishes)
        {
            output += $"{garnish}\n";
        }
        output += "----------SERVE----------";

        ingredientsText.text = output;
        
        RecipeManager.Instance.currentRecipe = data;  // saves actual recipe
        RecipeManager.Instance.InitIngredients();  // prepare blank ingredient checklist for player
    }
}
