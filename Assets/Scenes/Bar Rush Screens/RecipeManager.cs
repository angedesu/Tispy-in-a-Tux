using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RecipeManager : MonoBehaviour
{
    public static RecipeManager Instance;
    public DrinkRecipeData currentRecipe;
    
    // For showing ingredient progress in UI
    public Dictionary<string, bool> playerIngredients = new();
    
    // Player selections by category
    public List<string> alcohols = new();
    public List<string> mixers = new();
    public List<string> garnishes = new();

    void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else Destroy(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        alcohols.Clear();
        mixers.Clear();
        garnishes.Clear();
        
        playerIngredients.Clear();
        foreach (string ingredient in currentRecipe.ingredients)
            playerIngredients[ingredient] = false;
    }

    // this checks the players alcohol and player mixers from the recipe
    // this function will be called to check if users will be able to "mix"
    public bool CheckBaseIngredients()
    {
        // All required alcohols must be added
        foreach (string a in currentRecipe.alcohols)
        {
            if (!alcohols.Contains(a)) return false;
        }

        // All required mixers must be added
        foreach (string m in currentRecipe.mixers)
        {
            if (!mixers.Contains(m)) return false;
        }

        return true;
    }
    //used for drop and drag
    public void AddIngredient(string ingredient)
    {
        if (IngredientLibrary.Alcohols.Contains(ingredient))
        {
            if (!alcohols.Contains(ingredient)) alcohols.Add(ingredient);
            Debug.Log($"Added Alcohol: {ingredient}");
        }
        else if (IngredientLibrary.Mixers.Contains(ingredient))
        {
            if (!mixers.Contains(ingredient)) mixers.Add(ingredient);
            Debug.Log($"Added Mixer: {ingredient}");
        }
        else if (IngredientLibrary.Garnishes.Contains(ingredient))
        {
            if (!garnishes.Contains(ingredient)) garnishes.Add(ingredient);
            Debug.Log($"Added Garnish: {ingredient}");
        }
        else
        {
            Debug.LogWarning($"⚠Unknown ingredient: {ingredient}");
        }

        // Mark the overall ingredient progress (optional visual)
        MarkIngredient(ingredient);
    }


    // This checks players whole drink from the recipe
    // this function will be called to check if user will be able to "serve"
    public bool CheckFullIngredients()
    {
        // Checks if ALL ingredients from the recipe (alcohol, mixer, garnish) were added
        foreach (string ing in currentRecipe.ingredients)
        {
            if (!playerIngredients.ContainsKey(ing) || playerIngredients[ing] == false)
                return false;
        }
        return true;
    }

    // this function changes the players ingredients to true once a player drops an ingredient
    // in the mixer
    // helper function used for Addingredients()
    public void MarkIngredient(string ingredient)
    {
        if (playerIngredients.ContainsKey(ingredient))
            playerIngredients[ingredient] = true;
    }
    
    // this reset the mixer
    public void ResetMixer()
    {
        alcohols.Clear();
        mixers.Clear();

        foreach (var key in playerIngredients.Keys.ToList())
        {
            if (IngredientLibrary.Alcohols.Contains(key) || IngredientLibrary.Mixers.Contains(key))
            {
                playerIngredients[key] = false;
            }
        }
    }
    // this resets the garnishes
    public void ResetGarnishes()
    {
        garnishes.Clear();

        foreach (var key in playerIngredients.Keys.ToList())
        {
            if (IngredientLibrary.Garnishes.Contains(key))
            {
                playerIngredients[key] = false;
            }
        }
    }
}
