using UnityEngine;
using UnityEngine.SceneManagement;

public class logins : MonoBehaviour
{
    public void NextScreen()
    {
        // Change to actual screen
        SceneManager.LoadScene("profileSelection");
    }

}
