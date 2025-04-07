using UnityEngine;
using TMPro;
// this script gets the users from backend for friends section
public class friendItem : MonoBehaviour
{
    public TMP_Text usernameText;
    public TMP_Text idText;
    public TMP_Text levelText;
	
    private int gameID;
    private friendSystem friendSystem;

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
