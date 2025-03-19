using UnityEngine;
using UnityEngine.SceneManagement;

<<<<<<< Updated upstream
public class leaderboards : MonoBehaviour, IPointerDownHandler
{
    public GameObject unpressedButton;
    public GameObject pressedButton;

    public void NextScreen()
    {
        // change to the actual screen
        SceneManager.LoadScene("Leaderboard");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        unpressedButton.SetActive(false);
        pressedButton.SetActive(true);
        NextScreen();
=======
public class Leaderboards : MonoBehaviour
{
    public void NextScreen()
    {
        // change to the actual screen
        SceneManager.LoadScene("Scenes/Leaderboard/Leaderboard");
>>>>>>> Stashed changes
    }
}
