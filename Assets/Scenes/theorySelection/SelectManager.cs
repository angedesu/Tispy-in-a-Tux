using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
    public GameObject ingredientPanel;
    public GameObject techPanel;
    public GameObject toolsPanel;

    public void ingredientExpand()
    {
        ingredientPanel.SetActive(true);
    }

    public void techExpand()
    {
        techPanel.SetActive(true);
    }

    public void toolsExpand()
    {
        toolsPanel.SetActive(true);
    }

    public void ingredientShrinker()
    {
        ingredientPanel.SetActive(false);
    }

    public void techShrinker()
    {
        techPanel.SetActive(false);
    }

    public void toolsShrinker()
    {
        toolsPanel.SetActive(false);
    }

    public void ingredientSelected()
    {
        // change the scene to correct scene
        SceneManager.LoadScene("Scenes/theorySelection/ingredients/ingredients");
    }

    public void techSelected()
    {
        SceneManager.LoadScene("Scenes/theorySelection/techniques/techniques");
    }

    public void toolsSelected()
    {
        // change to correct scene
        SceneManager.LoadScene("Scenes/theorySelection/tools/tools");
    }
    
    public void Back()
    {
        SceneManager.LoadScene("Scenes/learnSelection/learnSelect");
    }
}
