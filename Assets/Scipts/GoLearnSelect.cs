using UnityEngine;
using UnityEngine.SceneManagement;

//namespace learnSelectionAfter
namespace learnSelectionAfter
{
    public class GoLearnSelect : MonoBehaviour
    {
        public void PreviousScreen()
        {
            // Use scene name or scene index 
            SceneManager.LoadScene("Scenes/learnSelection/learnSelect");
        }
    }
}
