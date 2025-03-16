using UnityEngine;
using UnityEngine.SceneManagement;

namespace Leaderboard
{
    public class arrowButton : MonoBehaviour
    {
        public void PreviousScreen()
        {
            // Use scene name or scene index
            UnityEngine.SceneManagement.Scene scene = SceneManager.GetActiveScene();
            Debug.Log("Active Scene is '" + scene.name + "'.");
            SceneManager.LoadScene("homeScreen");
        }
    }
}