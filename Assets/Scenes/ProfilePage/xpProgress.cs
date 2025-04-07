using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XpProgress : MonoBehaviour
{
    public int max = 100;
    public Image mask;
    public GameObject levelText;
    public TMP_Text usernameText;
    public TMP_Text gameidText;


    void Start()
    {
//        PlayerPrefs.DeleteKey("xpPoints");
//        PlayerPrefs.DeleteKey("xpLevel");
        PlayerPrefs.SetInt("xpPoints", 456);
 //       PlayerPrefs.Save();
        // get the current fill of the progress bar
        CurrentFill();
        UpdateLevelText();
        UserInfo();
    }

    void UserInfo()
    {
	    // set the username for the user
	    string username = UserSession.Username;
	    usernameText.text = username;
	    
	    // set the id for the user
	    int gameid = UserSession.GameID;
	    gameidText.text = "ID: " + gameid.ToString();
    }

    public void UpdateLevelText()
    {
        string number = PlayerPrefs.GetInt("xpLevel", 0).ToString();
        levelText.GetComponent<TMP_Text>().text = $"{number}";
        
        // UserSession.Level = int.Parse(number);
    }
    
	// user levels up when they hit 100 xp points and updates their level
	// we will be calling this function when a user gains xp
	public void AddXP(int gainXP)
	{
    	// Get current total XP
    	int currentXP = PlayerPrefs.GetInt("xpPoints", 0);
    	int currentLevel = PlayerPrefs.GetInt("xpLevel", 0);

    	// Add the new XP
    	int totalXP = currentXP + gainXP;

    	// Calculate new level based on total XP
    	int newLevel = totalXP / max;

    	// Update PlayerPrefs
		// Need to update the backend with this data
    	PlayerPrefs.SetInt("xpPoints", totalXP);
    	PlayerPrefs.SetInt("xpLevel", newLevel);
    	PlayerPrefs.Save();

    	// Update UI
    	UpdateLevelText();
    	CurrentFill();
	}
	public void CurrentFill()
	{
	    int points = PlayerPrefs.GetInt("xpPoints", 0);
    	int level = PlayerPrefs.GetInt("xpLevel", 0);

 	     // XP progress within this level
    	int newPoints = points % max;
	    // UserSession.XP = newPoints;

    	// fill the progress bar
    	float fillAmount = (float)newPoints / (float)max;
    	mask.fillAmount = fillAmount;
	}
}
