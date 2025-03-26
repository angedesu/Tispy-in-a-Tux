using UnityEngine;
using UnityEngine.SceneManagement;

namespace learnSelect
{
    public class arrowButton : MonoBehaviour
    {
        public void PreviousScreen()
        {
            SceneManager.LoadScene("Scenes/HomeScreen/homeScreen");
        }
    } 
}
