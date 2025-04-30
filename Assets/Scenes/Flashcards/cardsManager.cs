using UnityEngine;
using UnityEngine.SceneManagement;

public class cardsManager : MonoBehaviour
{
    public void GoBack()
    {
        SceneManager.LoadScene("learnSelect");
    }
}
