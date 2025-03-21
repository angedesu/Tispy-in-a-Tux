using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CheckIn : MonoBehaviour
{
    public void NextScreen()
    {
        SceneManager.LoadScene("checkInScreen");
    }
}
