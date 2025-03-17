using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class friends : MonoBehaviour, IPointerDownHandler
{
    public GameObject unpressedButton;
    public GameObject pressedButton;

    public void NextScreen()
    {
        // change to the actual scene name
        SceneManager.LoadScene("friendScreen");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        unpressedButton.SetActive(false);
        pressedButton.SetActive(true);
        NextScreen();
    }
}
