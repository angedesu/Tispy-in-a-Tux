using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using Newtonsoft.Json;
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

    // Prefabs for each tab
    public GameObject viewFriendItemPrefab;      // For viewing existing friends
    public GameObject addFriendItemPrefab;       // For addable users
    public GameObject requestFriendItemPrefab;   // For incoming requests

    public Transform friendListContainer;        // Shared container
    private GameObject currentPrefab;            // Prefab used for this tab

    void Start()
    {
        // Nothing for now; tab buttons call ShowFriends(), etc.
    }

    public void ShowFriends()
    {
        currentPrefab = viewFriendItemPrefab;
        ClearFriendListUI();
        StartCoroutine(GetFriends(UserSession.GameID));
    }

    public void ShowAddableUsers()
    {
        currentPrefab = addFriendItemPrefab;
        ClearFriendListUI();
        StartCoroutine(GetUsers(UserSession.GameID));
    }

    public void ShowRequests()
    {
        currentPrefab = requestFriendItemPrefab;
        ClearFriendListUI();
        StartCoroutine(GetRequests(UserSession.GameID));
    }

    private void ClearFriendListUI()
    {
        foreach (Transform child in friendListContainer)
        {
            Destroy(child.gameObject);
        }
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

    private IEnumerator GetUsers(int gameID)
    {
        string url = $"http://localhost:3000/non-friends/{gameID}";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error getting non-friend users: " + request.error);
        }
        else
        {
            string json = request.downloadHandler.text;
            List<Friend> users = JsonConvert.DeserializeObject<List<Friend>>(json);

            foreach (var user in users)
            {
                Debug.Log($"User - Username: {user.username}, GameID: {user.gameID}, Level: {user.level}");
                AddFriendToUI(user);
            }
        }
    }

    private IEnumerator GetRequests(int gameID)
    {
        string url = $"http://localhost:3000/received-requests/{gameID}";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error getting friend requests: " + request.error);
        }
        else
        {
            string json = request.downloadHandler.text;
            List<Friend> requests = JsonConvert.DeserializeObject<List<Friend>>(json);

            foreach (var req in requests)
            {
                Debug.Log($"Request From - Username: {req.username}, GameID: {req.gameID}");
                AddFriendToUI(req);
            }
        }
    }

    private void AddFriendToUI(Friend friend)
    {
        if (currentPrefab == null)
        {
            Debug.LogWarning("No prefab assigned for current tab!");
            return;
        }

        GameObject item = Instantiate(currentPrefab, friendListContainer);
        friendItem friendUI = item.GetComponent<friendItem>();
        if (friendUI != null)
        {
            friendUI.SetFriendData(friend.username, friend.gameID, friend.level);
        }
    }
}
