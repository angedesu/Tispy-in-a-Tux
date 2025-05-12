using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class FirstServeAchievement : MonoBehaviour
{
    const string FirstServeKey = "FirstServe";
    public string firstServeAchievementName = "Beginning" ;

    public void OnServeButtonClicked()
    {   
        Debug.Log("FirstServe Button Clicked");
        Debug.Log($"FirstServeKey int: {PlayerPrefs.GetInt(FirstServeKey)}");
        if (!AchievementCompleted())
        {
            MarkAchievementCompleted();
            StartCoroutine(UpdateAchievementDatabase(UserSession.GameID, firstServeAchievementName, 1));
            Debug.Log("First Serve Achievement update called");
        }
    }

    private bool AchievementCompleted()
    {
        return PlayerPrefs.GetInt(FirstServeKey, 0) == 1;
    }

    private void MarkAchievementCompleted()
    {
        PlayerPrefs.SetInt(FirstServeKey, 1);
        PlayerPrefs.Save();
        Debug.Log($"First serve achieved, FirstServeKey value: {PlayerPrefs.GetInt(FirstServeKey)}");
    }
    
    private class ProgressPayload
    {
        public int newProgress;
    }
    
    public IEnumerator UpdateAchievementDatabase(int gameID, string achievementName, int newProgress)
    {   
        Debug.Log($"Updating achievement database {achievementName}");
        string encodedName = UnityWebRequest.EscapeURL(achievementName);
        string url = $"http://localhost:3000/achievements/{gameID}/{encodedName}";

        // Create a payload with the expected key
        string jsonPayload = JsonUtility.ToJson(new ProgressPayload { newProgress = newProgress });
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonPayload);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Error updating achievement '{achievementName}' for user {gameID}: {request.error}");
        }
        else
        {
            Debug.Log($"Successfully updated achievement '{achievementName}' for user {gameID}. Response: {request.downloadHandler.text}");
        }
    }

    
}
