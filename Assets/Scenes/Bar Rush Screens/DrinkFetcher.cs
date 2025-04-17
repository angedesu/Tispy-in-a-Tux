using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class DrinkFetcher : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI ingredientsText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Search Options")]
    public string drinkToSearch = "margarita";

    [ContextMenu("Fetch Drink Info")] // for debug
    public void FetchDrinkInfo()
    {
        StartCoroutine(GetDrinkData(drinkToSearch));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GetDrinkData(string drinkName)
    {
        string url = $"https://www.thecocktaildb.com/api/json/v1/1/search.php?s={drinkName}";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + request.error);
            yield break;
        }
        string json = request.downloadHandler.text;
        JObject data = JObject.Parse(json);

        JToken drink = data["drinks"]?[0];
        if (drink == null)
        {
            Debug.LogWarning("No drink found.");
            yield break;
        }
        
        HashSet<string> drinkIngredients = new HashSet<string>();
        for (int i = 1; i <= 15; i++)
        {
            string key = $"strIngredient{i}";
            string ingredient = drink[key]?.ToString()?.Trim();

            if (!string.IsNullOrEmpty(ingredient))
            {
                drinkIngredients.Add(ingredient);
                Debug.Log($"- {ingredient}");
            }
        }
        //create list with all the ingredients
        List<string> allPossibleIngredients = new List<string>
        {
            // alcohol (24)
            "Tequila", "Triple sec", "Campari", "Sweet Vermouth", "Scotch", "Drambuie", "Brandy", "White Creme de Menthe",
            "Gin", "gin", "Bourbon", "Dry Vermouth", "Blended whiskey", "Light rum", "Cognac", "Cointreau", "Apricot brandy", "Apple brandy",
            "Creme de Cacao", "Benedictine", "Dark Rum", "Ricard", "Port", "Maraschino liqueur",
            // mixers (20)
            "Lime juice", "Angostura bitters", "Pineapple juice", "Grenadine", "Light cream", "Orange bitters",
            "Orange Juice", "Lemon juice", "Club soda", "Soda Water", "Sugar syrup", "Peach Bitters", "Peychaud bitters", "Water", "Cream",
            "Vanilla extract", "Carbonated water", "Egg white", "Egg Yolk", "Sugar",
            //garnish (13)
            "Salt", "Orange peel", "Lemon peel", "Olive", "Maraschino cherry", "Lemon", "Cherry", "Lime",
            "Powdered sugar", "Nutmeg", "Anis", "Orange", "Mint",
        };
        //create a dictionary for the drink ingredients. if ingredient in the drink return 1, else return 0
        Dictionary<string, int> allIngredients = new Dictionary<string, int>();
        foreach (string ingredient in allPossibleIngredients)
        {
            allIngredients[ingredient] = drinkIngredients.Contains(ingredient) ? 1 : 0;
        }
        // shows the list in console
        //foreach (var entry in allIngredients)
        //{
        //    Debug.Log($"{entry.Key}: {entry.Value}");
        //}
        
        // Set title
        if (titleText != null)
        {
            titleText.text = drink["strDrink"]?.ToString() ?? "Unknown Drink";
        }
        else
        {
            Debug.LogWarning("Title Text not assigned.");
        }
        string outputText = "";
        foreach (string ingredient in drinkIngredients)
        {
            outputText += $"- {ingredient}\n";
        }
        
        if (ingredientsText != null)
        {
            ingredientsText.text = outputText;
        }
        else
        {
            Debug.LogWarning("Ingredients Text not assigned.");
        }
    }
}
