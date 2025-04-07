using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FriendButtonSwitch : MonoBehaviour
{	

    // Main Containers
    public GameObject viewFriendsContainer;
    public GameObject addFriendsContainer;
    public GameObject requestFriendsContainer;

    public TextMeshProUGUI titleText;

	public friendSystem friendSystemScript;

    private enum FriendState { ViewFriends, AddFriends, SendRequest }
    private FriendState currentState;

    void Start()
    {
        // Default state to View Friends
        SetState(FriendState.ViewFriends);
		friendSystemScript.ShowFriends();
    }
    // shows ui for "Viewed friends"
    public void OnViewFriendsButtonClicked()
    {
        SetState(FriendState.ViewFriends);
		friendSystemScript.ShowFriends();
    }
    // shows ui for "to add a friend"
    public void OnAddFriendsButtonClicked()
    {
        SetState(FriendState.AddFriends);
		friendSystemScript.ShowAddableUsers();
    }
    // shows ui for "friend requests" sent from a user
    public void OnSendRequestButtonClicked()
    {
        SetState(FriendState.SendRequest);
        friendSystemScript.ShowRequests();
    }

    private void SetState(FriendState newState)
    {
        currentState = newState;
        UpdateUI();
    }
    // ui manager, when a button is beting clicked
    private void UpdateUI()
    {
		viewFriendsContainer.SetActive(currentState == FriendState.ViewFriends);
        addFriendsContainer.SetActive(currentState == FriendState.AddFriends);
        requestFriendsContainer.SetActive(currentState == FriendState.SendRequest);

        // Update title text
        switch (currentState)
        {
            case FriendState.ViewFriends:
                titleText.text = "Friends";
                break;
            case FriendState.AddFriends:
                titleText.text = "Add Friends";
                break;
            case FriendState.SendRequest:
                titleText.text = "Requests";
                break;
        }
    }
}
