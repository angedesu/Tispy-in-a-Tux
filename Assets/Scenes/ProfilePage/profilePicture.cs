using UnityEngine;
using UnityEngine.SceneManagement;

public class profilePicture : MonoBehaviour
{
    // add the new profile picture selection
    public void NextScreen()
    {
        SceneManager.LoadScene("profileSelection");
    }
}
