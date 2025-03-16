using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Leaderboard
{
    public class BrownButton : MonoBehaviour
    {
        public void Right()
        {
            // Use scene name or scene index 
            UnityEngine.SceneManagement.Scene scene = SceneManager.GetActiveScene();
            Debug.Log("Active Scene is '" + scene.name + "'.");
            SceneManager.LoadScene("Leaderboard");
        }
        public void Left()
        {
            // Use scene name or scene index 
            UnityEngine.SceneManagement.Scene scene = SceneManager.GetActiveScene();
            Debug.Log("Active Scene is '" + scene.name + "'.");
            SceneManager.LoadScene("Top3Leaderboard");
        }
    }
}