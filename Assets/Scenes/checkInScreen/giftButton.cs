using System;
using UnityEngine;
using UnityEngine.UI;

public class giftButton : MonoBehaviour
{
    public GameObject claimText;
    public GameObject claimedText;
    private string lastClaim = "Claim1";
    private string lastLogin = "Login1";
    
    
    private void Start()
    {
        CheckStatus();
        UpdateLogin();
    }

    void CheckStatus()
    {
        string lastClaimDate = PlayerPrefs.GetString("lastClaim", "");
        string today = DateTime.Today.ToString("MM/dd/yyyy");

        if (lastClaimDate == today)
        {
            // if they already claimed, do not display gift or claim text, display claimed text
            this.gameObject.SetActive(false);
            claimText.SetActive(false);
            claimedText.SetActive(true);
        }
        else
        {
            // display gift and do NOT display claimed text 
            this.gameObject.SetActive(true);
            claimText.SetActive(true);
            claimedText.SetActive(false);
        }
    }

    void UpdateLogin()
    {
        string today = DateTime.Today.ToString("MM/dd/yyyy");
        PlayerPrefs.SetString("lastLogin", today);
        PlayerPrefs.Save();
    }
    public void OnMouseDown()
    {
        // since clicked on gift, save todays date and update
        string today = DateTime.Today.ToString("MM/dd/yyyy");
        PlayerPrefs.SetString("lastClaim", today);
        PlayerPrefs.Save();
        // Stop displaying gift and show claimed text
        this.gameObject.SetActive(false);
        claimText.SetActive(false);
        claimText.SetActive(false);
    }
}
