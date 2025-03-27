using UnityEngine;
using TMPro;

public class ProfileUIManager : MonoBehaviour
{
    public TMP_Text usernameText;
    public TMP_Text levelText;
    public XpProgress xpProgressBar;

    void Start()
    {
        // load user info from UserSession
        string username = UserSession.Username;
        int level = UserSession.Level;
        int xp = UserSession.XP;

        // updates the UI
        usernameText.text = username;
        levelText.text = "Level: " + level;

        // updates the xp bar
        PlayerPrefs.SetInt("xpPoints", xp);
        PlayerPrefs.SetInt("xpLevel", level);
        PlayerPrefs.Save();

        xpProgressBar.CurrentFill();
        xpProgressBar.UpdateLevelText();
    }
}