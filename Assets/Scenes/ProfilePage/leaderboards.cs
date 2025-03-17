using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class leaderboards : MonoBehaviour, IPointerDownHandler
{
    public GameObject unpressedButton;
    public GameObject pressedButton;

    public void NextScreen()
    {
        // change to the actual screen
        SceneManager.LoadScene("checkInScreen");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        unpressedButton.SetActive(false);
        pressedButton.SetActive(true);
        NextScreen();
    }
}
