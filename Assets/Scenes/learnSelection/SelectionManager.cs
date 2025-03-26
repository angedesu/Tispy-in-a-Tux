using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionManager : MonoBehaviour
{
    public GameObject timerPanel;
    public GameObject theoryPanel;
    public GameObject cardsPanel;

    public void timerExpand()
    {
        timerPanel.SetActive(true);
    }

    public void theoryExpand()
    {
        theoryPanel.SetActive(true);
    }

    public void cardsExpand()
    {
        cardsPanel.SetActive(true);
    }

    public void timerShrinker()
    {
        timerPanel.SetActive(false);
    }

    public void theoryShrinker()
    {
        theoryPanel.SetActive(false);
    }

    public void cardsShrinker()
    {
        cardsPanel.SetActive(false);
    }

    public void timerSelected()
    {
        // change the scene to correct scene
        SceneManager.LoadScene("Scenes/HomeScreen/homeScreen");
    }

    public void theorySelected()
    {
        SceneManager.LoadScene("Scenes/theorySelection/theorySelect");
    }

    public void cardsSelected()
    {
        // change to correct scene
        SceneManager.LoadScene("Scenes/HomeScreen/homeScreen");
    }
}
