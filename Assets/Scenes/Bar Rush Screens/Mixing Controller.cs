using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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
    // public GameObject mixer;
    public Animator mixerAnim;
    public GameObject mixerContainer;
    public GameObject finish;
    public Animator finishAnim;
    public GameObject timesUpText;
    public GameObject finishedText;
    public GameObject start;
    public Animator startAnim;
    
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
        StartCoroutine(StartTriggered());
    }
    void Update()
    {
        // go to gameover screen
        if (GameStats.DrinksRemaining <= 0)
        {
            StartCoroutine(FinishTriggered());
            LoadGameOverScreen();
        }
        
        if (TimerManager.Instance != null && TimerManager.Instance.timeRemaining <= 0)
        {
            StartCoroutine(TimesUpTriggered());
            LoadGameOverScreen();
        }
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
        
        // Increment drink counter
        GameStats.DrinksRemaining--;

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
        // yield return null;
        // mixerAnim.enabled = true;
        mixerAnim.SetTrigger("mixer");
        yield return new WaitForSeconds(1.5f);
        // mixerAnim.Play("mixer");
    }

    private IEnumerator YuckTriggered()
    {
        yuck.SetActive(true);
        yuckAnim.SetTrigger("fadeIn");
        yield return null;
    }

    private IEnumerator ServedTriggered()
    {
        served.SetActive(true);
        servedAnim.SetTrigger("served");
        yield return null;
    }

    private IEnumerator StartTriggered()
    {
        start.SetActive(true);
        startAnim.SetTrigger("start");
        yield return null;
    }

    private IEnumerator FinishTriggered()
    {
        finish.SetActive(true);
        timesUpText.SetActive(false);
        finishedText.SetActive(true);
        finishAnim.SetTrigger("finish");
        yield return new WaitForSeconds(1.5f);
    }

    private IEnumerator TimesUpTriggered()
    {
        finish.SetActive(true);
        timesUpText.SetActive(true);
        finishedText.SetActive(false);
        finishAnim.SetTrigger("finish");
        yield return new WaitForSeconds(1.5f);
    }
    public Dictionary<string, GameObject> GetGlassMap()
    {
        return glassMap;
    }
    
    private void LoadGameOverScreen()
    {
        if (SceneManager.GetActiveScene().name == "MixingScreen")
        {
            SceneManager.LoadScene("GameOver Screen");
        }
    }
}
 