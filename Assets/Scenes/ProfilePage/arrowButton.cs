using UnityEngine;
using UnityEngine.SceneManagement;

namespace profile
{
    public class arrowButton : MonoBehaviour
    {
        public void PreviousScreen()
        {
            // Use scene name or scene index 
            SceneManager.LoadScene(6);
        }
    }
}
