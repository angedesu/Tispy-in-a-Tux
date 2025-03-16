using UnityEngine;
using UnityEngine.SceneManagement;

namespace Leaderboard
{
    public class brownButton : MonoBehaviour
    {
        public void Right()
        {
            // Use scene name or scene index 
            SceneManager.LoadScene("leaderboard");
        }
        public void Left()
        {
            // Use scene name or scene index 
            SceneManager.LoadScene("top3Leaderboard");
        }
    }
}