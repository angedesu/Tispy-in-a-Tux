using UnityEngine;
using UnityEngine.SceneManagement;

public class Achievements : MonoBehaviour
{
    public void NextScreen()
    {
        // change the scene to the achievments page (idk the actual name)
        SceneManager.LoadScene("checkInScreen");
    }
}
