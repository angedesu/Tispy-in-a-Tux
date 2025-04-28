using UnityEngine;
using TMPro;
using UnityEngine.UI;
// this script gets the users from backend for friends section
public class friendItem : MonoBehaviour
{
    public TMP_Text usernameText;
    public TMP_Text idText;
    public TMP_Text levelText;
	
    private int gameID;
    private friendSystem friendSystem;
    public Button deleteButton;
    public Button sendFriendRequestButton;
    public Button acceptFriendRequestButton;
    public Button requestFriendButton;

    public void Setup(string username, int id, int level, friendSystem system)
    {
		Debug.Log("Setting up: " + username);
    	if (usernameText == null) Debug.LogError("usernameText is NULL!");
    	if (idText == null) Debug.LogError("idText is NULL!");
    	if (levelText == null) Debug.LogError("levelText is NULL!");
    	if (system == null) Debug.LogError("friendSystem is NULL!");
        usernameText.text = username;
        idText.text = $"ID: {id}";
        levelText.text = $"Lvl: {level}";

        gameID = id;
        friendSystem = system;
        if (deleteButton != null)
        {
            deleteButton.onClick.RemoveAllListeners();
            deleteButton.onClick.AddListener(OnDeleteFriendClicked);
        }

        if (sendFriendRequestButton != null)
        {
            sendFriendRequestButton.onClick.RemoveAllListeners();
            sendFriendRequestButton.onClick.AddListener(OnSendFriendRequestClicked);
        }

        if (acceptFriendRequestButton != null)
        {
            acceptFriendRequestButton.onClick.RemoveAllListeners();
            acceptFriendRequestButton.onClick.AddListener(OnAcceptFriendRequestClicked);
        }

        if (requestFriendButton != null)
        {
            requestFriendButton.onClick.RemoveAllListeners();
            requestFriendButton.onClick.AddListener(OnRejectFriendRequestClicked);
        }
    }

    public void OnSendFriendRequestClicked()
    {
        friendSystem.SendFriendRequest(gameID);
    }

    public void OnAcceptFriendRequestClicked()
    {
        friendSystem.AcceptFriendRequest(gameID);
    }

    public void OnRejectFriendRequestClicked()
    {
        friendSystem.RejectFriendRequest(gameID);
    }

    public void OnDeleteFriendClicked()
    {
        friendSystem.DeleteFriend(gameID);
    }
}
