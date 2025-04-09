using UnityEngine;
using UnityEngine.SceneManagement;

namespace RecipeBookSearch
{
    public class ArrowButton : MonoBehaviour
    {
        public void PreviousScreen()
        {
            // Use scene name or scene index 
            SceneManager.LoadScene("Scenes/HomeScreen/homeScreen");
        }
    }
}
