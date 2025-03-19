using UnityEngine;
using UnityEngine.SceneManagement;

namespace CheckInScreen
{
    public class ArrowButton : MonoBehaviour
    {
        public void PreviousScreen()
        {
            // Use scene name or scene index 
            SceneManager.LoadScene("profile");
        }
    }
}
