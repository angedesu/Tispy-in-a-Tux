using UnityEngine;
using TMPro;

public class friendItem : MonoBehaviour
{
    public TMP_Text usernameText;
    public TMP_Text idText;
    public TMP_Text levelText;

    public void SetFriendData(string username, int gameID, int level)
    {
        usernameText.text = username;
        idText.text = $"ID: {gameID}";
        levelText.text = $"Lvl: {level}";
        Debug.Log($"SetFriendData called: {username}, {gameID}, {level}");

    }
}
