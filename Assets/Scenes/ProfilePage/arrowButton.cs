using UnityEngine;
using UnityEngine.SceneManagement;

namespace profile
{
    public class ArrowButton : MonoBehaviour
    {
        public void PreviousScreen()
        {
            // Use scene name or scene index 
            SceneManager.LoadScene("Scenes/learnSelection/learnSelect");
        }
    }
}
