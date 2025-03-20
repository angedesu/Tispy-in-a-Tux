using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoginSceneLoader : MonoBehaviour, IPointerDownHandler
{
    public GameObject unpressedButton;
    public GameObject pressedButton;
    public GameObject dimBackground;

    public string overlaySceneName = "Sign-in"; // Change this to the actual scene name

    private void LoadOverlayScene()
    {
        SceneManager.LoadScene(overlaySceneName, LoadSceneMode.Additive);
        if (dimBackground != null)
        {
            dimBackground.SetActive(true); // Show dim background
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Switch button visuals
        unpressedButton.SetActive(false);
        pressedButton.SetActive(true);
        
        // Load the overlay scene
        LoadOverlayScene();
        
    }
}