using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XpProgress : MonoBehaviour
{
    public int max = 100;
    public Image mask;
    public GameObject levelText;
    void Start()
    {
//        PlayerPrefs.DeleteKey("xpPoints");
//        PlayerPrefs.DeleteKey("xpLevel");
 //       PlayerPrefs.SetInt("xpPoints", 456);
 //       PlayerPrefs.Save();
        // get the current fill of the progress bar
        CurrentFill();
        UpdateLevelText();
        
    }

    public void UpdateLevelText()
    {
        string number = PlayerPrefs.GetInt("xpLevel", 0).ToString();
        levelText.GetComponent<TMP_Text>().text = $"{number}";
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

    	// fill the progress bar
    	float fillAmount = (float)newPoints / (float)max;
    	mask.fillAmount = fillAmount;
	}
}
