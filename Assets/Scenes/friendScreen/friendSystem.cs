using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text;
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

    }
	// shows list of friends
    public void ShowFriends()
    {
        currentPrefab = viewFriendItemPrefab;
        ClearFriendListUI();
        StartCoroutine(GetFriends(UserSession.GameID));
    }
	// shows all users that have not been added or in "friend requests"
    public void ShowAddableUsers()
    {
        currentPrefab = addFriendItemPrefab;
        ClearFriendListUI();
        StartCoroutine(GetUsers(UserSession.GameID));
    }
	// shows users that sent a friend request
    public void ShowRequests()
    {
        currentPrefab = requestFriendItemPrefab;
        ClearFriendListUI();
        StartCoroutine(GetRequests(UserSession.GameID));
    }
	// clears list ui
    private void ClearFriendListUI()
    {
        foreach (Transform child in friendListContainer)
        {
            Destroy(child.gameObject);
        }
    }
	// gets all friends from backend api
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
	// gets all users from backend api
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
	// gets all the users who has requested the user (you)
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
	// this adds the prefab from unity to the friends
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
            friendUI.Setup(friend.username, friend.gameID, friend.level, this);
        }
    }
    // Send friend request from current user to another user
    public void SendFriendRequest(int toGameID)
    {
        StartCoroutine(PostRequest("http://localhost:3000/send-friend-request", new
        {
            fromGameID = UserSession.GameID,
            toGameID = toGameID
        }, "Friend request sent"));
    }
    // Accept a friend request from another user
    public void AcceptFriendRequest(int fromGameID)
    {
        StartCoroutine(PostRequest("http://localhost:3000/accept-friend-request", new
        {
            fromGameID = fromGameID,
            toGameID = UserSession.GameID
        }, "Friend request accepted"));
    }
    // Reject a friend request from another user
    public void RejectFriendRequest(int fromGameID)
    {
        StartCoroutine(PostRequest("http://localhost:3000/reject-friend-request", new
        {
            fromGameID = fromGameID,
            toGameID = UserSession.GameID
        }, "Friend request rejected"));
    }
    // Delete a friend
    public void DeleteFriend(int friendGameID)
    {
        StartCoroutine(PostRequest("http://localhost:3000/delete-friend", new
        {
            userGameID = UserSession.GameID,
            friendGameID = friendGameID
        }, "Friend removed"));
    }
    // Generic POST helper
    private IEnumerator PostRequest(string url, object bodyObject, string successMessage)
    {
        string jsonBody = JsonConvert.SerializeObject(bodyObject);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Error calling {url}: {request.error}");
            Debug.LogError("Response: " + request.downloadHandler.text);
        }
        else
        {
            Debug.Log(successMessage);
        }

    }
}
