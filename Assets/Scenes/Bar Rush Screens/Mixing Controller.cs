using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MixingController : MonoBehaviour
{
    [Header("Glass Prefabs")] public GameObject highballGlass;
    public GameObject cocktailGlass;
    public GameObject oldFashionedGlass;
    public GameObject collinsGlass;

    public GameObject served;
    public Animator servedAnim;
    public GameObject yuck;
    public Animator yuckAnim;
    public Animator mixerAnim;
    public GameObject mixerContainer;
    public GameObject finish;
    public Animator finishAnim;
    public GameObject timesUpText;
    public GameObject finishedText;
    public GameObject start;
    public Animator startAnim;

    public GameObject settingsParent;

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
            Debug.Log("FINISHED TRIGGER");
            // LoadGameOverScreen();
        }

        if (TimerManager.Instance != null && TimerManager.Instance.timeRemaining <= 0)
        {
            StartCoroutine(TimesUpTriggered());
            Debug.Log("TIME UP TRIGGER");
            // LoadGameOverScreen();
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
        {
            // StartCoroutine(TurnOnMixer());
            mixerContainer.SetActive(true);
        }

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
        // StartCoroutine(MixerTriggered());
        // yield return new WaitForSeconds(1f);

        if (mixerContainer != null)
            mixerContainer.SetActive(false);
            // StartCoroutine(TurnOffMixer());

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
        mixerAnim.enabled = true;
        yield return new WaitForSeconds(1f);
        mixerAnim.SetTrigger("mixer");
        // yield return new WaitForSeconds(2f);
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
        yield return new WaitForSeconds(1.5f);
    }

    private IEnumerator StartTriggered()
    {
        start.SetActive(true);
        startAnim.SetTrigger("start");
        yield return null;
    }

    public IEnumerator FinishTriggered()
    {
        finish.SetActive(true);
        Debug.Log("You did it!");
        timesUpText.SetActive(false);
        finishedText.SetActive(true);
        finishAnim.SetTrigger("finished");
        yield return new WaitForSeconds(2f);
        // yield return null;
        LoadGameOverScreen();
    }

    public IEnumerator TimesUpTriggered()
    {
        finish.SetActive(true);
        Debug.Log("Times up!");
        timesUpText.SetActive(true);
        finishedText.SetActive(false);
        finishAnim.SetTrigger("finished");
        yield return new WaitForSeconds(2f);
        // yield return null;
        LoadGameOverScreen();
    }

    private IEnumerator TurnOffMixer()
    {
        mixerContainer.SetActive(false); 
        yield return null;
    }

    private IEnumerator TurnOnMixer()
    {
        mixerAnim.enabled = false;
        mixerContainer.SetActive(true);
        // mixerAnim.Play("Idle", -1, 0f);
        //yield return new WaitForSeconds(2f);
        yield return new WaitForSeconds(0.5f);
        // mixerAnim.enabled = false;
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

    private IEnumerator QuitGameOver()
    {
        settingsParent.SetActive(false);
        finish.SetActive(true);
        timesUpText.SetActive(false);
        finishedText.SetActive(false);
        finishAnim.SetTrigger("finished");
        yield return new WaitForSeconds(1f);
        LoadGameOverScreen();
    }

    public void QuitGame()
    {
        StartCoroutine(QuitGameOver());
    }

    public void MenuButton()
    {
        settingsParent.SetActive(true);
    }
}
 