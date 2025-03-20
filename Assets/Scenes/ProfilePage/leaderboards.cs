using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Leaderboards : MonoBehaviour, IPointerDownHandler
{
    public GameObject unpressedButton;
    public GameObject pressedButton;

    private void NextScreen()
    {
        // change to the actual screen
        SceneManager.LoadScene("Scenes/Leaderboard/Top3Leaderboard");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        unpressedButton.SetActive(false);
        pressedButton.SetActive(true);
        NextScreen();
    }
}
