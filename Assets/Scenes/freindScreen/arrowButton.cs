using UnityEngine;
using UnityEngine.SceneManagement;

namespace friendScreen
{
    public class arrowButton : MonoBehaviour
    {
        public void nextScene()
        {
            SceneManager.LoadScene("Scenes/ProfilePage/profile");
        }
    }

}
