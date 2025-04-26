using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class MixingController : MonoBehaviour
{
    public Animator shakerAnimator;
    public GameObject correctGlass;
    public GameObject mixerContainer;
    public GameObject incorrectPopup;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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

    private IEnumerator PerformMixing()
    {
        if (shakerAnimator != null)
            shakerAnimator.SetTrigger("Mixer");

        yield return new WaitForSeconds(1.5f);

        if (mixerContainer != null) mixerContainer.SetActive(false);
        if (correctGlass != null) correctGlass.SetActive(true);

        Debug.Log("Drink is ready!");
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
