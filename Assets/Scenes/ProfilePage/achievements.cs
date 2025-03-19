using UnityEngine;
using UnityEngine.SceneManagement;

<<<<<<< Updated upstream
public class achievements : MonoBehaviour, IPointerDownHandler
{
    public GameObject unpressedButton;
    public GameObject pressedButton;

=======
public class Achievements : MonoBehaviour
{
>>>>>>> Stashed changes
    public void NextScreen()
    {
        // change the scene to the achievments page (idk the actual name)
        SceneManager.LoadScene("checkInScreen");
    }
}
