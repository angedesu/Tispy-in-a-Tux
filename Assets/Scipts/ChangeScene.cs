using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene: MonoBehaviour
{
    public void GoToHomeScreen()
    {
        SceneManager.LoadScene("Scenes/HomeScreen/homeScreen");
    }

    public void GoToStartScreen()
    {
        SceneManager.LoadScene("Start Screen");
    }
    
        
}
