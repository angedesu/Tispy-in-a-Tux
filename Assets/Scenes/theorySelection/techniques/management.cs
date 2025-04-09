using UnityEngine;
using UnityEngine.SceneManagement;

public class management : MonoBehaviour
{
    public void PreviousScreen()
    {
        SceneManager.LoadScene("Scenes/theorySelection/theorySelect");
    }
}
