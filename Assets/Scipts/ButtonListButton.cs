using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonListButton : MonoBehaviour
{
    public Button helpButton;
    [SerializeField] private TMP_Text myText;
    private string topicDescription;

    public void SetText(string topicTitle, string description)
    {
        myText.text = topicTitle;
        topicDescription = description;
        helpButton.onClick.RemoveAllListeners();
        helpButton.onClick.AddListener(OnHelpClick);
    }

    public void OnClick()
    {
        //Debug.Log("Selected topic: " + myText.text);
    }

    public void OnHelpClick()
    {
        TopicPopupController.Instance.ShowPopup(topicDescription);
    }
}
