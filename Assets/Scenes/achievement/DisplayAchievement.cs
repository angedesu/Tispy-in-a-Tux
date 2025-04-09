using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using Newtonsoft.Json;
using TMPro;

[System.Serializable]
public class Achievement
{
    public string name;
    public string description;
    public int progress;
    public int target;
}

[System.Serializable]
public class AchievementResponse
{
    public List<Achievement> achievements;
}

public class DisplayAchievement : MonoBehaviour
{
    public Transform achievementListContainer;
    public GameObject achievementPrefab;
    // Keep track of the displayed achievements
    public Dictionary<string, AchievementItem> displayedAchievements = new Dictionary<string, AchievementItem>(); 
    
    
    // Keys for keeping track of achievement
    private const string FirstLaunchKey = "FirstLaunchDetector";
    
    void Start()
    {   
        // Preventing duplicates
        ClearAchievementListUI();
        displayedAchievements.Clear();
        
        // Get achievements
        StartCoroutine(GetAchievements(UserSession.GameID));
        
        // Process achievements
        StartCoroutine(WaitForAchievements());

    }

    // Get user achievements from database
    private IEnumerator GetAchievements(int gameID)
    {
        string url = $"http://localhost:3000/achievements/{gameID}";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error getting achievements:" + request.error);
        }
        else
        {
            string json = request.downloadHandler.text;
            AchievementResponse response = JsonConvert.DeserializeObject<AchievementResponse>(json);

            foreach (var achievement in response.achievements)
            {
                AddAchievementToUI(achievement);
            }
            Debug.Log("Got the achievements:");
        }
    }

    private void AddAchievementToUI(Achievement achievement)
    {
        GameObject achievementItemInstantiate = Instantiate(achievementPrefab, achievementListContainer);
        AchievementItem item = achievementItemInstantiate.GetComponent<AchievementItem>();

        if (item != null)
        {
            item.InitializeAchievement(achievement.name, achievement.description, achievement.progress, achievement.target);
            displayedAchievements[achievement.name] = item;
        }
    }
    
    // Preventing duplicates
    private void ClearAchievementListUI()
    {
        foreach (Transform child in achievementListContainer)
        {
            Destroy(child.gameObject);
        }
    }
    
    // Update the database with user progress
    private IEnumerator UpdateAchievementDatabase(int gameID, string achievementName, int newProgress)
    {
        string url = $"http://localhost:3000/achievements/{gameID}/{achievementName}";
        
        string jsonPayload = JsonUtility.ToJson(new { progress = newProgress });
        byte[] body = System.Text.Encoding.UTF8.GetBytes(jsonPayload);
        
        UnityWebRequest request = UnityWebRequest.Put(url, body);
        request.SetRequestHeader("Content-Type", "application/json");
        
        yield return request.SendWebRequest();
        
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Error updating achievement '{achievementName}' for user {gameID}: {request.error}");
        }
        else
        {
            Debug.Log($"Successfully updated achievement '{achievementName}' for user {gameID} to progress: {newProgress}. Response: {request.downloadHandler.text}");
        }
        
        
    }
    
    // Check and Process achievements
    private IEnumerator WaitForAchievements()
    {
        // Wait for to populate displayedAchievements
        yield return null;
        FirstLaunchAchievement();
    }
    
    private void FirstLaunchAchievement()
    {   
        Debug.Log("FirstLaunchAchievement() called");
        if (PlayerPrefs.GetInt(FirstLaunchKey, 0) == 1)
        {
            if (displayedAchievements.ContainsKey("Welcome!"))
            {
                displayedAchievements["Welcome!"].UpdateProgress(1);
                StartCoroutine(UpdateAchievementDatabase(UserSession.GameID, "Welcome!", 1));
                PlayerPrefs.SetInt(FirstLaunchKey, 2);  // Change key
                Debug.Log("first launch key changed");
                PlayerPrefs.Save();
            }
            else
            {
                Debug.LogError("Welcome not found in displayed achievement list");
            }
        }
    }
    
    
    
    

}
