using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Achievements : MonoBehaviour, IPointerDownHandler
{
    public GameObject unpressedButton;
    public GameObject pressedButton;

    private void NextScreen()
    {
        // change the scene to the achievements page
        SceneManager.LoadScene("Scenes/achievement/Achievement");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        unpressedButton.SetActive(false);
        pressedButton.SetActive(true);
        NextScreen();
    }
}
