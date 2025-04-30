using TMPro;
using UnityEngine;

public class TopicPopupController : MonoBehaviour
{
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private TMP_Text popupText;

    public static TopicPopupController Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void ShowPopup(string description)
    {
        popupText.text = description;
        popupPanel.SetActive(true);
    }

    public void HidePopup()
    {
        popupPanel.SetActive(false);
    }
}


