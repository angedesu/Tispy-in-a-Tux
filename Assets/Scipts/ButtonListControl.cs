using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListControl : MonoBehaviour
{
    [SerializeField] private GameObject buttonTemplate;
    [SerializeField] private FlashcardsReviewController reviewController;
    [SerializeField] private switchActivation switcher;


    private List<string> topicsList;
    private Dictionary<string, string> topicDescriptions;

    void Start()
    {
        topicsList = new List<string> {
            "Back Bar", "Bartending Jargon", "Color Chart", "Customer Service",
            "Glassware", "Speed Gun", "Top Back Bar", "Two Liquor Drinks", "Wines", "+"
        };

        topicDescriptions = new Dictionary<string, string>() {
            { "Back Bar", "Bottles on the shelves behind the bar." },
            { "Bartending Jargon", "Common terms used by bartenders." },
            { "Color Chart", "Visual guide for drink colors." },
            { "Customer Service", "Best practices for guest interaction." },
            { "Glassware", "Types of bar glassware and use." },
            { "Speed Gun", "Multi-dispenser used for soda and mixers." },
            { "Top Back Bar", "Premium spirits often displayed high up." },
            { "Two Liquor Drinks", "Cocktails with two base spirits." },
            { "Wines", "Basic wine types and service tips." },
            { "+", "Create or add a new topic." }
        };

        for (int t = 0; t < topicsList.Count; t++)
        {
            GameObject button = Instantiate(buttonTemplate);
            button.SetActive(true);

            string topicTitle = topicsList[t];
            string description = topicDescriptions.ContainsKey(topicTitle) ? topicDescriptions[topicTitle] : "";

            ButtonListButton blb = button.GetComponent<ButtonListButton>();
            blb.SetText(topicTitle, description);
            blb.reviewController = reviewController;
            blb.switcher = switcher;


            button.GetComponent<Button>().onClick.RemoveAllListeners();
            button.GetComponent<Button>().onClick.AddListener(blb.OnClick);

            button.transform.SetParent(buttonTemplate.transform.parent, false);
        }
    }
}
