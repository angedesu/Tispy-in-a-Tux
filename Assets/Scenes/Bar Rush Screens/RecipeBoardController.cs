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

        titleText.text = data.title;
        ingredientsText.text = "";
        foreach (string ingredient in data.ingredients)
        {
            ingredientsText.text += $"- {ingredient}\n";
        }
        
        //RecipeManager.Instance.currentRecipe = data;  // saves actual recipe
        // RecipeManager.Instance.InitIngredients();  // prepare blank ingredient checklist for player
    }
}
