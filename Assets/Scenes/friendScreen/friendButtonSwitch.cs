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

    public void OnViewFriendsButtonClicked()
    {
        SetState(FriendState.ViewFriends);
		friendSystemScript.ShowFriends();
    }

    public void OnAddFriendsButtonClicked()
    {
        SetState(FriendState.AddFriends);
		friendSystemScript.ShowAddableUsers();
    }

    public void OnSendRequestButtonClicked()
    {
        SetState(FriendState.SendRequest);
		//friendSystemScript.ShowAddableUsers();
    }

    private void SetState(FriendState newState)
    {
        currentState = newState;
        UpdateUI();
    }

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
