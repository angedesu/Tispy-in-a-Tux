using UnityEngine;
using UnityEngine.SceneManagement;

namespace CheckInScreen
{
    public class arrowButton : MonoBehaviour
    {
        public void PreviousScreen()
        {
            // Use scene name or scene index 
            SceneManager.LoadScene("profileSelection");
        }
    }
}
