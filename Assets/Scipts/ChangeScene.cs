using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene: MonoBehaviour
{
    public void GoToHomeScreen()
    {
        SceneManager.LoadScene("Home Screen");
    }

    public void GoToStartScreen()
    {
        SceneManager.LoadScene("Start Screen");
    }
    
        
}
