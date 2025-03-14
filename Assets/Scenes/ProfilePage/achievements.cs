using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class achievements : MonoBehaviour, IPointerDownHandler
{
    public GameObject unpressedButton;
    public GameObject pressedButton;

    public void NextScreen()
    {
        // change the scene to the achievments page (idk the actual name)
        SceneManager.LoadScene("checkInScreen");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        unpressedButton.SetActive(false);
        pressedButton.SetActive(true);
        NextScreen();
    }
}
