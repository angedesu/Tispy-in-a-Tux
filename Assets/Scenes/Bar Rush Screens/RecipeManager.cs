using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RecipeManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static RecipeManager Instance;
    public DrinkRecipeData currentRecipe;
    public Dictionary<string, bool> playerIngredients = new();

    void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else Destroy(gameObject);
    }

    // got started on some of the functions we will be using for the game mode.
    // it is a good roadmap of how our game will function when called.
    // when a player starts the game, it will create a player ingredients dictionary
    // which is used to compare the recipes for drinks that have to be made.
    
    // still figuring out if we need to create 3 separate lists for the
    // ingredients: alcohols, mixers, garnishes.
    
    // this function creates the users dictionary and returns false on all ingredients
    public void InitIngredients()
    {
        playerIngredients.Clear();
        foreach (string ingredient in currentRecipe.ingredients)
            playerIngredients[ingredient] = false;
    }

    // this checks the players alcohol and player mixers from the recipe
    // this function will be called to check if users will be able to "mix"
    public bool CheckBaseIngredients(List<string> playerAlcohols, List<string> playerMixers)
    {
        return playerAlcohols.All(i => currentRecipe.ingredients.Contains(i)) &&
               playerMixers.All(i => currentRecipe.ingredients.Contains(i));
    }

    // This checks players whole drink from the recipe
    // this function will be called to check if user will be able to "serve"
    public bool CheckFullIngredients()
    {
        return playerIngredients.All(kvp => kvp.Value == true);
    }

    // this function changes the players ingredients to true once a player drops an ingredient
    // in the mixer
    // probably used this for drag and drop ingredients
    public void MarkIngredient(string ingredient)
    {
        if (playerIngredients.ContainsKey(ingredient))
            playerIngredients[ingredient] = true;
    }
    
    // this reset the mixer
    public void ResetMixer()
    {
        foreach (var key in playerIngredients.Keys.ToList())
            playerIngredients[key] = false;
    }
}
