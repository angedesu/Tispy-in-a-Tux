using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Linq;
using System.Collections;
using Newtonsoft.Json.Linq;
using Unity.VisualScripting;
using UnityEngine.Rendering;
using System;
using RecipeBook;
using TMPro;

public class LoadRecipe : MonoBehaviour
{
    public Text name;
    public int ID;
    private string url = $"https://www.thecocktaildb.com/api/json/v1/1/lookup.php?i=";
    public RecipeBook.RecipeBook bookSearch;
    public GameObject SearchBar;
    public GameObject DrinkContainer;
    public GameObject RecipeDetails;
    public RawImage drinkImage;
    public TextMeshProUGUI nameTitle;
    public TextMeshProUGUI drinkIngredients;
    public TextMeshProUGUI drinkInstructions;
    public TextMeshProUGUI drinkGlass;
    public void Start()
    {
        ID = bookSearch.GetRecipeID(name.text);
    }
    public void HideSearchUI()
    {
        //Hides the search UI
        SearchBar.SetActive(false);
        DrinkContainer.SetActive(false);
        RecipeDetails.SetActive(true);
    }
    public void LoadDrinkInformation()
    {
        //Needs to call a subroutine
        StartCoroutine(LoadDrinkInfo());
    }
    private IEnumerator LoadDrinkInfo()
    {
        //Actually connects to the database
        string target = url + ID;
        UnityWebRequest request = UnityWebRequest.Get(target);
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            //Request failed
            Debug.LogError(request.error);
            yield break;
        }
        JObject drinkData = JObject.Parse(request.downloadHandler.text);
        if (drinkData == null)
        {
            //Invalid Drink
            yield break;
        }
        foreach (JToken drink in drinkData["drinks"]) {
            //Create the information page
            nameTitle.text = name.text;
            string ingredients = "";
            //Ingredients
            int i = 1;
            do
            {
                string ingredient = "";
                ingredient = drink["strIngredient" + i]!.ToString();
                if (ingredient != "" && i == 1)
                {
                    ingredients = ingredient;
                }
                i++;
                if (ingredient != "")
                {
                    ingredients = ingredients + ", " + ingredient;
                }
                else
                {
                    break;
                }
            } while (i < 16);
            drinkIngredients.text = ingredients;
            //Instructions
            string instructions = "";
            instructions = drink["strInstructions"]!.ToString();
            drinkInstructions.text = instructions;
            //Glass
            string glass = "";
            glass = drink["strGlass"]!.ToString();
            drinkGlass.text = glass;
            //Get the image and load it
            string drinkURL = "";
            drinkURL = drink["strDrinkThumb"]!.ToString();
            drinkURL = drinkURL + "/large";
            UnityWebRequest requestTexture = UnityWebRequestTexture.GetTexture(drinkURL);
            yield return requestTexture.SendWebRequest();
            if (requestTexture.result != UnityWebRequest.Result.Success)
            {
                //Request failed
                Debug.Log("Image failed to load");
                Debug.LogError(requestTexture.error);
            }
            Texture drinkTexture = DownloadHandlerTexture.GetContent(requestTexture);
            drinkImage.texture = drinkTexture;
            HideSearchUI();
        }
    }
}
