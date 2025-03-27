using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Auth;
using UnityEngine.SceneManagement;

public class loginToggleButton : MonoBehaviour
{
    public TMP_Text buttonText;
    private Button button;
    private FirebaseAuth auth;

    void Start()
    {
        button = GetComponent<Button>();
        auth = FirebaseAuth.DefaultInstance;

        UpdateButton();
    }

    void UpdateButton()
    {
        if (auth.CurrentUser != null)
        {
            // user is logged in → Show Logout
            buttonText.text = "Logout";
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(Logout);
        }
        else
        {
            // user is not logged in → Show Login
            buttonText.text = "Login";
            button.onClick.RemoveAllListeners();
        }
    }

    void Logout()
    {
        auth.SignOut();

        // Clear session values if needed
        PlayerPrefs.DeleteAll();
        UserSession.Username = "Please Sign In";
        UserSession.Level = 0;
        UserSession.XP = 0;

        // Reload the current scene to reflect logout changes
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}