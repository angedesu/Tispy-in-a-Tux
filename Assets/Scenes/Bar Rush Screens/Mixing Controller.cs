using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class MixingController : MonoBehaviour
{
    [Header("Glass Prefabs")]
    public GameObject highballGlass;
    public GameObject cocktailGlass;
    public GameObject oldFashionedGlass;
    public GameObject collinsGlass;
    
    public Animator shakerAnimator;
    public GameObject mixerContainer;
    public GameObject incorrectPopup;

    private Dictionary<string, GameObject> glassMap;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        glassMap = new Dictionary<string, GameObject>
        {
            { "Highball glass", highballGlass },
            { "Cocktail glass", cocktailGlass },
            { "Old-fashioned glass", oldFashionedGlass },
            { "Collins glass", collinsGlass }
        };
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Mix()
    {
        if (RecipeManager.Instance.CheckBaseIngredients())
        {
            Debug.Log("Correct ingredients! Mixing...");
            StartCoroutine(PerformMixing());
        }
        else
        {
            Debug.Log("Incorrect mix.");
            ShowIncorrectFeedback();
            RecipeManager.Instance.ResetMixer();
        }
    }
    
    public void Serve()
    {
        if (!RecipeManager.Instance.CheckFullIngredients())
        {
            Debug.LogWarning("Garnishes incorrect. Please fix garnish ingredients!");

            // (Optional) Show a visual popup or feedback to user
            if (incorrectPopup != null)
            {
                incorrectPopup.SetActive(true);
                Invoke(nameof(HideIncorrectFeedback), 2f);
            }
        
            return;
        }

        // Drink is correct ✅
        Debug.Log("Drink served successfully!");

        // Increment drink counter (assuming you have something like GameStats.DrinksServed)
        GameStats.DrinkServed--;

        // Show the mixer container again (for next drink)
        if (mixerContainer != null)
            mixerContainer.SetActive(true);

        // Hide any glasses that were shown
        foreach (var glass in glassMap.Values)
            glass.SetActive(false);

        // Load a new random recipe
        RecipeBoardController recipeBoard = FindAnyObjectByType<RecipeBoardController>();
        if (recipeBoard != null)
        {
            recipeBoard.ShowRandomDrink();
        }
    }

    private IEnumerator PerformMixing()
    {
        if (shakerAnimator != null)
            shakerAnimator.SetTrigger("Mixer");

        yield return new WaitForSeconds(1.5f);

        if (mixerContainer != null)
            mixerContainer.SetActive(false);

        string glassType = RecipeManager.Instance.currentRecipe.glassName;

        // Hide all glasses first
        foreach (var glass in glassMap.Values)
            glass.SetActive(false);

        // Activate the correct one
        if (glassMap.ContainsKey(glassType))
        {
            glassMap[glassType].SetActive(true);
            Debug.Log("Activated glass: " + glassType);
        }
        else
        {
            Debug.LogWarning($"No glass found for: {glassType}");
        }
    }

    private void ShowIncorrectFeedback()
    {
        if (incorrectPopup != null)
        {
            incorrectPopup.SetActive(true);
            Invoke(nameof(HideIncorrectFeedback), 2f);
        }
    }

    private void HideIncorrectFeedback()
    {
        if (incorrectPopup != null)
            incorrectPopup.SetActive(false);
    }
}
