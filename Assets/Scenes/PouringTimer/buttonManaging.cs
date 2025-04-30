using UnityEngine;
using UnityEngine.SceneManagement;
public class buttonManaging : MonoBehaviour
{
    public void PreviousScreen()
    {
        SceneManager.LoadScene("Scenes/learnSelection/learnSelect");
    }
}
