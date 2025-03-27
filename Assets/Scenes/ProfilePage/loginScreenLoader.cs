using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginSceneLoader : MonoBehaviour
{
    public GameObject dimBackground;
    public string overlaySceneName = "Sign-in";
    
    public void LoadOverlayScene()
    {
        SceneManager.LoadScene(overlaySceneName, LoadSceneMode.Additive);
        if (dimBackground != null)
        {
            dimBackground.SetActive(true); // Show dim background
        }
    }
}
