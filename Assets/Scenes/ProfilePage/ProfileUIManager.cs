using UnityEngine;
using TMPro;

public class ProfileUIManager : MonoBehaviour
{
    public TMP_Text usernameText;
    public TMP_Text levelText;
    public XpProgress xpProgressBar;

    void Start()
    {
        // Load from UserSession
        string username = UserSession.Username;
        int level = UserSession.Level;
        int xp = UserSession.XP;

        // Update UI
        usernameText.text = username;
        levelText.text = "Level: " + level;

        // Update XP Progress Bar
        PlayerPrefs.SetInt("xpPoints", xp);
        PlayerPrefs.SetInt("xpLevel", level);
        PlayerPrefs.Save();

        xpProgressBar.CurrentFill();
        xpProgressBar.UpdateLevelText();
    }
}