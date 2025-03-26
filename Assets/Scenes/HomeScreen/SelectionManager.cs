using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectedManager : MonoBehaviour
{
    public void playSelected()
    {
        // replace with correct scene
        SceneManager.LoadScene("playScene");
    }

    public void recipeSelected()
    {
        // replace with corrent scene
        SceneManager.LoadScene("recipeScene");
    }

    public void toolsSelected()
    {
        SceneManager.LoadScene("Scenes/learnSelection/learnSelect");
    }
}
