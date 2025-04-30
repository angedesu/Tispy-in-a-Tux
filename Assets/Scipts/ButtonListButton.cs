using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ButtonListButton : MonoBehaviour
{
    public FlashcardsReviewController reviewController;
    public switchActivation switcher;

    public Button helpButton;
    [SerializeField] private TMP_Text myText;
    private string topicTitle;
    private string topicDescription;

    public void SetText(string topicTitle, string description)
    {
        this.topicTitle = topicTitle;
        myText.text = topicTitle;
        topicDescription = description;

        helpButton.onClick.RemoveAllListeners();
        helpButton.onClick.AddListener(OnHelpClick);
    }

    public void OnClick()
    {
        Debug.Log("Topic clicked: " + topicTitle);
        //reviewController.LoadFlashcards(topicTitle);
        Debug.Log("Topic clicked: " + topicTitle + ", reviewController is null: " + (reviewController == null));
        reviewController.LoadFlashcards(topicTitle); // still updates content
        switcher.SwitchToGameObject2();              // switches from Home to Review
    }



    public void OnHelpClick()
    {
        TopicPopupController.Instance.ShowPopup(topicDescription);
    }
}
