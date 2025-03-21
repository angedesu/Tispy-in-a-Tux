using UnityEngine;
using UnityEngine.SceneManagement;

public class Friends : MonoBehaviour
{
    public void NextScreen()
    {
        // change to the actual scene name
        SceneManager.LoadScene("friendScreen");
    }
}
