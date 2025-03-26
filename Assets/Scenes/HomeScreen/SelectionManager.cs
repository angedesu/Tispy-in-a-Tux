using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectedManager : MonoBehaviour
{
    public void playSelected()
    {
        // replace with correct scene
        SceneManager.LoadScene("Scenes/gameSelection/gameSelect");
    }

    public void recipeSelected()
    {
        // replace with corrent scene
        SceneManager.LoadScene("Scenes/ProfilePage/profile");
    }

    public void toolsSelected()
    {
        SceneManager.LoadScene("Scenes/learnSelection/learnSelect");
    }
}
