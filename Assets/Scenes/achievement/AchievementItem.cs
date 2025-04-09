using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AchievementItem : MonoBehaviour
{
    [Header("Text Elements")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI statusText;
    
    
    [Header("Progress Bar")]
    public Slider progressBar;

    [Header("Checkmark")] 
    public GameObject uncheckMark;
    public GameObject checkMark;

    // Initial creation of the achievement item
    public void InitializeAchievement(string achievementName, string achievementDescription, int progress, int target)
    {
        if (nameText != null)
        {
            nameText.text = achievementName;
        }

        if (descriptionText != null)
        {
            descriptionText.text = achievementDescription;
        }
        
        statusText.text = $"{progress}/{target}";
        
        progressBar.minValue = 0;
        progressBar.maxValue = target;
        progressBar.value = progress;

        if (progress >= target)
        {
            uncheckMark.SetActive(false);
            checkMark.SetActive(true);
        }
    }
    
    // Update achievement progress
    public void UpdateProgress(int newProgress)
    {
        progressBar.value = newProgress;

        statusText.text = $"{newProgress}/{progressBar.maxValue}";

        if (newProgress >= progressBar.maxValue)
        {
            uncheckMark.SetActive(false);
            checkMark.SetActive(true);
        }
        
    }
    
}
