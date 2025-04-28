using UnityEngine;
using UnityEngine.SceneManagement;

namespace RecipeBookSearch
{
    public class ArrowButton : MonoBehaviour
    {
        public GameObject SearchBar;
        public GameObject DrinkContainer;
        public GameObject RecipeDetails;
        public void GoBack()
        {
            //If screen is search, call Previous Screen
            if (!RecipeDetails.activeInHierarchy)
            {
                PreviousScreen();
                return;
            }
            //Otherwise reenable the search GUI, and hide the details GUI
            RecipeDetails.SetActive(false);
            DrinkContainer.SetActive(true);
            SearchBar.SetActive(true);
        }
        private void PreviousScreen()
        {
            // Use scene name or scene index 
            SceneManager.LoadScene("Scenes/HomeScreen/homeScreen");
        }
        private void EnableSearch()
        {
            //Reenable the search GUI
        }
    }
}
