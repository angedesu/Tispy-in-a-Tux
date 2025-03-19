using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

<<<<<<< Updated upstream
public class checkIn : MonoBehaviour, IPointerDownHandler
{
    public GameObject unpressedButton;
    public GameObject pressedButton;
    
=======
public class CheckIn : MonoBehaviour
{
>>>>>>> Stashed changes
    public void NextScreen()
    {
        SceneManager.LoadScene("checkInScreen");
    }
}
