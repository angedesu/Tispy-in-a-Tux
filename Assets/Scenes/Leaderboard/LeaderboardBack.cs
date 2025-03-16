using UnityEngine;
using UnityEngine.SceneManagement;

namespace Leaderboard
{
    public class arrowButton : MonoBehaviour
    {
        public void PreviousScreen()
        {
            // Use scene name or scene index 
            SceneManager.LoadScene("homeScreen");
        }
    }
}