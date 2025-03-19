using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Friends : MonoBehaviour, IPointerDownHandler
{
    public GameObject unpressedButton;
    public GameObject pressedButton;

    private void NextScreen()
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
