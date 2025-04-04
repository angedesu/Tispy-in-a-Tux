using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.UI;
using TMPro;

public class friendSystem : MonoBehaviour
{
    [System.Serializable]
    public class Friend
    {
        public string username;
        public int gameID;
        public int level;
    }
    public GameObject friendItemPrefab; // Assign in Inspector
    public Transform friendListContainer; // The parent where items will be added (e.g., a vertical layout group)
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int currentGameID = UserSession.GameID;
        StartCoroutine(GetFriends(currentGameID));
    }

    private IEnumerator GetFriends(int gameID)
    {
        string url = $"http://localhost:3000/friends/{gameID}";
        UnityWebRequest request = UnityWebRequest.Get(url);
        
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error getting friends: " + request.error);
        }
        else
        {
            string json = request.downloadHandler.text;
            
            List<Friend> friends = JsonConvert.DeserializeObject<List<Friend>>(json);

            foreach (var friend in friends)
            {
                Debug.Log($"Friend - Username: {friend.username}, GameID: {friend.gameID}, Level: {friend.level}");
                AddFriendToUI(friend);
            }
        }
    }

    private void AddFriendToUI(Friend friend)
    {
        GameObject item = Instantiate(friendItemPrefab, friendListContainer);
        
        friendItem friendUI = item.GetComponent<friendItem>();
        friendUI.SetFriendData(friend.username, friend.gameID, friend.level);
        
    }
}
