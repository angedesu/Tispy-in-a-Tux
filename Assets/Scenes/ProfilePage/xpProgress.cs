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
        PlayerPrefs.DeleteKey("xpPoints");
        PlayerPrefs.DeleteKey("xpLevel");
        PlayerPrefs.SetInt("xpPoints", 139);
        PlayerPrefs.Save();
        // get the current fill of the progress bar
        CurrentFill();
        UpdateLevelText();
        
    }

    public void UpdateLevelText()
    {
        string number = PlayerPrefs.GetInt("xpLevel", 0).ToString();
        levelText.GetComponent<TMP_Text>().text = $"{number}";
    }
    int PlayerLevel(int points)
    {
        int level = 0;
        
        // first case: 200 % 100 == 0 but it would be level 2
        if (points % max == 0)
        {
            level = points / max;
        } 
        // second case: 123 % 100 = 23 but it is level 1
        // so we try 223/100 = 2.23. 2.23 > 1 so we find the floor division
        // 223 // 100 = 2 meaning they are in level 2
        else if ((float)points / max > 1)
        {
            level = Mathf.FloorToInt((float)points / max);
        }
        // third case: points % max is not equal to 0 => points are not divisible by 100 
        //                      AND
        // points / max is not greater than 1 => points does not have hundreds 
        // SO, points is between 0 - 100, it is level 0
        else
        {
            level = 0;
        }
        return level;
    }
    void UpdateLevel(int number1, int number2)
    {
        // update the xp level on file for the player
        int level = number1 + number2;
        PlayerPrefs.SetInt("xpLevel", level);
        PlayerPrefs.Save();
    }
    void UpdatePoints(int points)
    {
        // update the points on file for the player
        PlayerPrefs.SetInt("xpPoints", points);
        PlayerPrefs.Save();
    }
    void CurrentFill()
    {
        // get the points and level on file, find the new levels that will be added to the current levels
        int points = PlayerPrefs.GetInt("xpPoints", 0);
        int oldLevel = PlayerPrefs.GetInt("xpLevel", 0);
        int currentLevel = PlayerLevel(points);

        // calculate the points that will be used for the progress bar, update the player's level, and 
        // update the player's points
        int newPoints = points % max;
        UpdateLevel(currentLevel, oldLevel);
        UpdatePoints(newPoints);
        
        // fill the progress bar with the player's new points calculated
        float fillAmount = (float)newPoints / (float)max;
        mask.fillAmount = fillAmount;
    }
}
