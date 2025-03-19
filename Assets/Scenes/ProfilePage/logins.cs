using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Logins : MonoBehaviour, IPointerDownHandler
{
    public GameObject unpressedButton;
    public GameObject pressedButton;
    public GameObject loginPanel;
    

    void Start()
    {
        // check if user has logged in => display the logout button
    }
    
    private void NextScreen()
    {
        // overlay the sign-in page 
        loginPanel.SetActive(true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        unpressedButton.SetActive(false);
        pressedButton.SetActive(true);
        NextScreen();
    }
}
