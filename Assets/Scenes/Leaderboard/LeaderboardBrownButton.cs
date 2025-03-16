using UnityEngine;
using UnityEngine.SceneManagement;

namespace Leaderboard
{
    public class BrownButton : MonoBehaviour
    {
        public void Right()
        {
            // Use scene name or scene index 
            SceneManager.LoadScene("Leaderboard");
        }
        public void Left()
        {
            // Use scene name or scene index 
            SceneManager.LoadScene("Top3Leaderboard");
        }
    }
}