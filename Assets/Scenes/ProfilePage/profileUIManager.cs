using UnityEngine;
using TMPro;

public class ProfileUIManager : MonoBehaviour
{
    public TMP_Text usernameText;
    public TMP_Text levelText;
    public XpProgress xpProgressBar;
    public TMP_Text gameidText;

    void Start()
    {
        // load user info from UserSession
        string username = UserSession.Username;
        int level = UserSession.Level;
        int xp = UserSession.XP;
        int gameid = UserSession.GameID;

        // updates the UI
        usernameText.text = username;
        levelText.text = "Level: " + level;

        // updates the xp bar
        PlayerPrefs.SetInt("xpPoints", xp);
        PlayerPrefs.SetInt("xpLevel", level);
        PlayerPrefs.Save();
        
        // updates id text 
        gameidText.text = "ID: " + gameid;

        xpProgressBar.CurrentFill();
        xpProgressBar.UpdateLevelText();
    }
}