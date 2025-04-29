using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeSelector : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetEasy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetEasy()
    {
        GameSettings.selectedTime = 300f; // 5 min
        Debug.Log("Easy mode selected");
    }

    public void SetMedium()
    {
        GameSettings.selectedTime = 210f; // 3:30
        Debug.Log("Medium mode selected");
    }

    public void SetHard()
    {
        GameSettings.selectedTime = 150f; // 2:30
        Debug.Log("Hard mode selected");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("MixingScreen");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Scenes/HomeScreen/homeScreen");
    }
}
