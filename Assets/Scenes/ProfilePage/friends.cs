using UnityEngine;
using UnityEngine.SceneManagement;

<<<<<<< Updated upstream
public class friends : MonoBehaviour, IPointerDownHandler
{
    public GameObject unpressedButton;
    public GameObject pressedButton;

=======
public class Friends : MonoBehaviour
{
>>>>>>> Stashed changes
    public void NextScreen()
    {
        // change to the actual scene name
        SceneManager.LoadScene("friendScreen");
    }
}
