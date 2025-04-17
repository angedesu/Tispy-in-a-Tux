using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using System.Collections;

public class DrinkFetcher : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Search Options")]
    public string drinkToSearch = "margarita"; // This will show in the Inspector

    [ContextMenu("Fetch Drink Info")] // This lets you right-click and trigger from Inspector
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
        }
        else
        {
            string json = request.downloadHandler.text;
            JObject data = JObject.Parse(json);

            JToken drink = data["drinks"]?[0];
            if (drink != null)
            {
                string name = drink["strDrink"]?.ToString();
                Debug.Log($"Drink Name: {name}");
                
                Debug.Log("Ingredients:");
                for (int i = 0; i <= 15; i++)
                {
                    string key = $"strIngredient{i}";
                    string ingredient = drink[key]?.ToString();

                    if (!string.IsNullOrEmpty(ingredient))
                    {
                        Debug.Log($"- {ingredient}");
                    }
                    else
                    {
                        Debug.LogWarning("No drink found with that name.");
                    }
                }
            }
        }
        
        

    }
}
