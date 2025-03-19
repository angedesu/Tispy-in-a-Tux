using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CheckIn : MonoBehaviour, IPointerDownHandler
{
    public GameObject unpressedButton;
    public GameObject pressedButton;
    
    private void NextScreen()
    {
        SceneManager.LoadScene("checkInScreen");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        unpressedButton.SetActive(false);
        pressedButton.SetActive(true);
        NextScreen();
    }
}
