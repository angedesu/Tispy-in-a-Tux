using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckIn : MonoBehaviour
{
    public void NextScreen()
    {
        SceneManager.LoadScene("checkInScreen");
    }
}
