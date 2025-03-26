using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectorManager : MonoBehaviour
{
    public GameObject battlePanel;
    public GameObject rushPanel;
    public GameObject nightPanel;

    public void battleExpand()
    {
        battlePanel.SetActive(true);
    }

    public void rushExpand()
    {
        rushPanel.SetActive(true);
    }

    public void nightExpand()
    {
        nightPanel.SetActive(true);
    }

    public void battleShrinker()
    {
        battlePanel.SetActive(false);
    }

    public void rushShrinker()
    {
        rushPanel.SetActive(false);
    }

    public void nightShrinker()
    {
        nightPanel.SetActive(false);
    }

    public void battleSelected()
    {
        // change to screen
        SceneManager.LoadScene("Scenes/HomeScreen/homeScreen");
    }
    
    public void rushSelected()
    {
        // change to screen
        SceneManager.LoadScene("Scenes/HomeScreen/homeScreen");
    }
    
    public void nightSelected()
    {
        // change to screen
        SceneManager.LoadScene("Scenes/HomeScreen/homeScreen");
    }

    public void PreviousScreen()
    {
        SceneManager.LoadScene("Scenes/HomeScreen/homeScreen");
    }
}
