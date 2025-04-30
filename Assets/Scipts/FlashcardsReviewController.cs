using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlashcardsReviewController : MonoBehaviour
{
    [SerializeField] private GameObject flashcardsReviewPanel;
    [SerializeField] private GameObject homePanel;

    [SerializeField] private TMP_Text topicTitleText;
    [SerializeField] private TMP_Text frontText;
    [SerializeField] private TMP_Text backText;

    private List<(string front, string back)> flashcards;
    private int currentIndex = 0;
    private bool showingFront = true;

    private Dictionary<string, List<(string front, string back)>> topicFlashcards;

    void Start()
    {
        topicFlashcards = new Dictionary<string, List<(string, string)>>(){
            { "Glassware", new List<(string, string)>{  
                ("What glass is used for champagne?", "Flute"),
                ("What is a rocks glass?", "Short tumbler for spirits")
            }
        },
            { "Customer Service", new List<(string, string)>{
                ("What's most important when greeting a guest?", "Make eye contact and smile."),
                ("How to handle a complaint?", "Listen, apologize, and resolve quickly.")
            }
        },
        // Add more topics here...
    };
    }

    public void LoadFlashcards(string topicName)
    {
        topicTitleText.text = topicName;

        if (topicFlashcards.ContainsKey(topicName))
        {
            flashcards = topicFlashcards[topicName];
        }
        else
        {
            flashcards = new List<(string, string)> { ("No flashcards found.", "") };
        }

        currentIndex = 0;
        showingFront = true;
        ShowCard();
        //flashcardsReviewPanel.SetActive(true);
        gameObject.SetActive(true);
        homePanel.SetActive(false);
    }


    private void ShowCard()
    {
        if (flashcards == null || flashcards.Count == 0)
        {
            frontText.text = "No cards";
            backText.text = "";
            return;
        }

        (string front, string back) = flashcards[currentIndex];
        frontText.text = front;
        backText.text = back;
        backText.gameObject.SetActive(false); // start with front
    }

    public void NextCard()
    {
        if (flashcards == null || flashcards.Count <= 1) return;

        currentIndex = (currentIndex + 1) % flashcards.Count;
        showingFront = true;
        ShowCard();
    }

    public void PrevCard()
    {
        if (flashcards == null || flashcards.Count <= 1) return;

        currentIndex = (currentIndex - 1 + flashcards.Count) % flashcards.Count;
        showingFront = true;
        ShowCard();
    }

    public void FlipCard()
    {
        showingFront = !showingFront;
        frontText.gameObject.SetActive(showingFront);
        backText.gameObject.SetActive(!showingFront);
    }
}

