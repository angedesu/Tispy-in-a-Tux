using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListControl : MonoBehaviour
{
    [SerializeField] private GameObject buttonTemplate;
    private List<string> topicsList;

    Dictionary<string, string> topicDescriptions;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //make sure to implement "Color Chart", "Glassware", "Speed Gun", "Two Liquor Drinks", "Wines", "+"
        topicsList = new List<string> { "Back Bar", "Bartending Jargon", "Color Chart", "Customer Service", 
                                        "Glassware", "Speed Gun", "Top Back Bar", "Two Liquor Drinks", "Wines", "+"};
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

        for (int t = 0; t < topicsList.Count; t++) { 
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);
            
            string topicTitle = topicsList[t];
            string description = topicDescriptions.ContainsKey(topicTitle) ? topicDescriptions[topicTitle] : "";

            ButtonListButton blb = button.GetComponent<ButtonListButton>();
            blb.SetText(topicTitle, description);

            //Button helpBtn = button.transform.Find("questionMark").GetComponent<Button>();
            //helpBtn.onClick.AddListener(blb.OnHelpClick);

            button.transform.SetParent(buttonTemplate.transform.parent, false);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    //void GenerateButton() { 
    //
    //}


}
