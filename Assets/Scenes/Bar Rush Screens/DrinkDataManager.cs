using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;

public class DrinkDataManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator GetDrinkData(string drinkName, System.Action<DrinkRecipeData> callback)
    {
        string url = $"https://www.thecocktaildb.com/api/json/v1/1/search.php?s={drinkName}";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
            callback(null);
            yield break;
        }

        JObject data = JObject.Parse(request.downloadHandler.text);
        JToken drink = data["drinks"]?[0];
        if (drink == null)
        {
            callback(null);
            yield break;
        }
        
        DrinkRecipeData recipe = new DrinkRecipeData
        {
            title = drink["strDrink"]!.ToString(),
            ingredients = new List<string>()
        };

        for (int i = 1; i <= 15; i++)
        {
            string ingredient = drink[$"strIngredient{i}"]?.ToString()?.Trim();
            if (!string.IsNullOrEmpty(ingredient))
            {
                recipe.ingredients.Add(ingredient);
            }
        }

        callback(recipe);
    }
}
public class DrinkRecipeData
{
    public string title;
    public List<string> ingredients;
}
