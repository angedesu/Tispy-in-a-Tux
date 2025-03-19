using System;
using TMPro;
using UnityEngine;

public class giftButton : MonoBehaviour
{
    public GameObject claimText;
    public GameObject claimedText;
    public GameObject claimedGift;
    public GameObject streakNumber;
    
    private void Start()
    {
        // for testing
        // PlayerPrefs.DeleteAll();
        
        // on start, check if the gift has been opened today
        CheckGiftStatus();
        // on start, display the current streak number, before they press the gift 
        CheckStreakNumber();
    }
    public void OnClick()
    {
        // since clicked on gift, save today's date and update the last time they claimed
        string today = DateTime.Today.ToString("MM/dd/yyyy");
        PlayerPrefs.SetString("lastClaim", today);
        PlayerPrefs.Save();
        
        // Stop displaying gift and show claimed text and gift
        this.gameObject.SetActive(false);
        claimText.SetActive(false);
        claimedText.SetActive(true);
        claimedGift.SetActive(true);
        
        // update login
        UpdateLogin();
    }
    void CheckGiftStatus()
    {
        // create variables for the last time they claimed and today's date
        string lastClaimDate = PlayerPrefs.GetString("lastClaim", "");
        string today = DateTime.Today.ToString("MM/dd/yyyy");
        
        if (lastClaimDate == today)
        {
            // if they already claimed, do not display gift or claim text, display claimed text and gift
            this.gameObject.SetActive(false);
            claimText.SetActive(false);
            claimedText.SetActive(true);
            claimedGift.SetActive(true);
        }
        else
        {
            // have not claimed, display gift and claim text and do NOT display claimed text and gift
            this.gameObject.SetActive(true);
            claimText.SetActive(true);
            claimedText.SetActive(false);
            claimedGift.SetActive(false);
        }
    }

    void CheckStreakNumber()
    { 
        // find the last streak number saved and display that number instead of default (0)
        string lastStreak = PlayerPrefs.GetString("streakNumber", "0");
        streakNumber.GetComponent<TMP_Text>().text = lastStreak;
    }
    
    void UpdateLogin()
    {
        // create variable for today's date and save it as the lastLogin (latest) date
        string today = DateTime.Today.ToString("MM/dd/yyyy");
        PlayerPrefs.SetString("lastLogin", today);
        PlayerPrefs.Save();
        
        // retrieve the latest login and create a variable for today 
        string lastLogin = PlayerPrefs.GetString("lastLogin", "");
        DateTime now = DateTime.Today;
        
        // subtract today and last login () to get how long ago they claimed in days
        TimeSpan span = now - DateTime.Parse(lastLogin);
        int days = span.Days;
        
        // time span is integers so 1.5 days is 1 day and 12 hours NOT 1.5 days... half a day is not 0.5 days, instead 
        // it is 0 days and 12 hours! 
        
        if (days >= 1)
        {
            // if their last claim was 1 or more days ago => lost streak, reset streak to 1 (the new streak)
            streakNumber.GetComponent<TMP_Text>().text = "1";
        }
        else if (days == 0)
        {
            // if their last claim was less than one day ago => streak intact, increment streak by 1
            int number = int.Parse(streakNumber.GetComponent<TMP_Text>().text);
            int newNumber = number + 1;
            streakNumber.GetComponent<TMP_Text>().text = $"{newNumber}";
        }
        UpdateStreakNumber();
    }

    void UpdateStreakNumber()
    {
        string number = streakNumber.GetComponent<TMP_Text>().text;
        PlayerPrefs.SetString("streakNumber", number);
        PlayerPrefs.Save();
    }
}
