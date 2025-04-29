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
    
    public GameObject served;
    public Animator servedAnim;
    public GameObject yuck; 
    public Animator yuckAnim;
    public GameObject mixer;
    public Animator mixerAnim;
    public GameObject mixerContainer;
    public GameObject finish;
    public Animator finishAnim;
    
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
            StartCoroutine(YuckTriggered());
            RecipeManager.Instance.ResetMixer();
        }
    }
    
    public void Serve()
    {
        if (!RecipeManager.Instance.CheckFullIngredients())
        {
            Debug.LogWarning("Garnishes incorrect. Please fix garnish ingredients!");
            StartCoroutine(YuckTriggered());
            return;
        }

        // Drink is correct ✅
        Debug.Log("Drink served successfully!");
        
        // animation 
        StartCoroutine(ServedTriggered());
        
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
        StartCoroutine(MixerTriggered());
        yield return new WaitForSeconds(1.5f);
        Debug.Log("WAIT");
        StartCoroutine(ServedTriggered());
        
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
        
        yield return null;
    }

    private IEnumerator MixerTriggered()
    {
        // yield return new WaitForSeconds(1.5f);
        yuckAnim.enabled = true;
        yield return null;
        mixerAnim.SetTrigger("mixer");
        yuckAnim.enabled = false;
    }

    private IEnumerator YuckTriggered()
    {
        // yield return new WaitForSeconds(1.5f);
        yuck.SetActive(true);
        yuckAnim.SetTrigger("fadeIn");
        yield return null;
    }

    private IEnumerator ServedTriggered()
    {
        // yield return new WaitForSeconds(1.5f);
        served.SetActive(true);
        servedAnim.SetTrigger("served");
        yield return null;
    }

    private IEnumerator FinishTriggered()
    {
        // yield return new WaitForSeconds(1.5f);
        finish.SetActive(true);
        finishAnim.SetTrigger("finish");
        yield return null;
    }
    public Dictionary<string, GameObject> GetGlassMap()
    {
        return glassMap;
    }
}
 