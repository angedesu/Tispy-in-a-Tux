using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProfileSelection
{
    public class arrowButton : MonoBehaviour
    {
        public void PreviousScreen()
        {
            // Use scene name or scene index
            SceneManager.LoadScene("checkInScreen");
        }
    }
}

