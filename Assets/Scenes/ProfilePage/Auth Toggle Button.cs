using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Auth;
using UnityEngine.SceneManagement;

public class AuthToggleButton : MonoBehaviour
{
    public TMP_Text buttonText; // Reference to the loginText object

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
            // ✅ User is logged in → Show Logout
            buttonText.text = "Logout";
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(Logout);
        }
        else
        {
            // ❌ User is not logged in → Show Login
            buttonText.text = "Login";
            button.onClick.RemoveAllListeners();
        }
    }

    void Logout()
    {
        auth.SignOut();

        // Clear session values if needed
        PlayerPrefs.DeleteAll();
        UserSession.Username = "";
        UserSession.Level = 0;
        UserSession.XP = 0;

        // Reload the current scene to reflect logout changes
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}