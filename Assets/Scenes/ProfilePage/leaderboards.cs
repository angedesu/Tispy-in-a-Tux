using UnityEngine;
using UnityEngine.SceneManagement;

public class Leaderboards : MonoBehaviour
{
    public void NextScreen()
    {
        // change to the actual screen
        SceneManager.LoadScene("Scenes/Leaderboard/Leaderboard");
    }
}
