using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FriendButtonSwitch : MonoBehaviour
{
    public GameObject viewFriendsContainer;
    public GameObject addFriendsContainer;
    public GameObject sendRequestContainer;
    public TextMeshProUGUI titleText;

    private enum FriendState { ViewFriends, AddFriends, SendRequest }
    private FriendState currentState;

    void Start()
    {
        // Default state to View Friends
        SetState(FriendState.ViewFriends);
    }

    public void OnViewFriendsButtonClicked()
    {
        SetState(FriendState.ViewFriends);
    }

    public void OnAddFriendsButtonClicked()
    {
        SetState(FriendState.AddFriends);
    }

    public void OnSendRequestButtonClicked()
    {
        SetState(FriendState.SendRequest);
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
        sendRequestContainer.SetActive(currentState == FriendState.SendRequest);
        
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