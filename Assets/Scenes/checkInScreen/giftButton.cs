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
        // PlayerPrefs.DeleteAll();
        CheckGiftStatus();
        UpdateLogin();
        CheckStreakNumber();
    }
    void CheckGiftStatus()
    {
        string lastClaimDate = PlayerPrefs.GetString("lastClaim", "");
        string today = DateTime.Today.ToString("MM/dd/yyyy");

        if (lastClaimDate == today)
        {
            // if they already claimed, do not display gift or claim text, display claimed text
            this.gameObject.SetActive(false);
            claimText.SetActive(false);
            claimedText.SetActive(true);
            claimedGift.SetActive(true);
        }
        else
        {
            // have not claimed, display gift and do NOT display claimed text 
            this.gameObject.SetActive(true);
            claimText.SetActive(true);
            claimedText.SetActive(false);
            claimedGift.SetActive(false);
        }
    }

    void CheckStreakNumber()
    { 
        string lastStreak = PlayerPrefs.GetString("streakNumber", "0");
        
        streakNumber.GetComponent<TMP_Text>().text = lastStreak;
    }
    void UpdateLogin()
    {
        string today = DateTime.Today.ToString("MM/dd/yyyy");
        PlayerPrefs.SetString("lastLogin", today);
        PlayerPrefs.Save();
    }
    public void OnClick()
    {
        // since clicked on gift, save today's date and update
        string today = DateTime.Today.ToString("MM/dd/yyyy");
        PlayerPrefs.SetString("lastClaim", today);
        PlayerPrefs.Save();
        // Stop displaying gift and show claimed text
        this.gameObject.SetActive(false);
        claimText.SetActive(false);
        claimedText.SetActive(true);
        claimedGift.SetActive(true);
        
        CheckLogin();
    }
    void CheckLogin()
    {
        string LastLogin = PlayerPrefs.GetString("lastLogin", "");
        DateTime today = DateTime.Today;
        // string today = DateTime.Today.ToString("MM/dd/yyyy");
        // DateTime todayDate = DateTime.Parse(today);
        
        // subtract today and last login ()
        TimeSpan span = today - DateTime.Parse(LastLogin);
        int days = span.Days;
        
        if (days > 1)
        {
            // more than 1 day => lost streak, reset streak
            streakNumber.GetComponent<TMP_Text>().text = "1";
        }
        else if (days == 1)
        {
            // one day apart => streak intact, increment streak by 1
            streakNumber.GetComponent<TMP_Text>().text += "1";
        }
        else
        {
            // reset counter to 1, 
            streakNumber.GetComponent<TMP_Text>().text = "1";
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
