using UnityEngine;
using UnityEngine.SceneManagement;

public class manager : MonoBehaviour
{
    public void PreviousScreen()
    {
        SceneManager.LoadScene("Scenes/theorySelection/theorySelect");
    }
}
