using UnityEngine;
using UnityEngine.SceneManagement;

public class Achievements : MonoBehaviour
{
    public void NextScreen()
    {
        // change the scene to the achievements page
        SceneManager.LoadScene("Scenes/achievement/Achievement");
    }

}
