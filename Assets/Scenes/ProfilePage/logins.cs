using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class logins : MonoBehaviour, IPointerDownHandler
{
    public GameObject unpressedButton;
    public GameObject pressedButton;
    
    public void NextScreen()
    {
        // Change to actual screen
        SceneManager.LoadScene("profileSelection");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        unpressedButton.SetActive(false);
        pressedButton.SetActive(true);
        NextScreen();
    }
}
