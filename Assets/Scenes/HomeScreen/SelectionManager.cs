using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SelectedManager : MonoBehaviour
{
    public void Start()
    {
        // update the login
        UpdateLogin();
    }

    void UpdateLogin()
    {
        // create variable for today's date and save it as the lastLogin (latest) date
        string today = DateTime.Today.ToString("MM/dd/yyyy");
        PlayerPrefs.SetString("lastLogin", today);
        PlayerPrefs.Save();
    }
    public void playSelected()
    {
        // replace with correct scene
        SceneManager.LoadScene("Scenes/gameSelection/gameSelect");
    }

    public void recipeSelected()
    {
        // replace with corrent scene
        SceneManager.LoadScene("Scenes/RecipeBook/RecipeBookSearch");
    }

    public void toolsSelected()
    {
        SceneManager.LoadScene("Scenes/learnSelection/learnSelect");
    }
}
