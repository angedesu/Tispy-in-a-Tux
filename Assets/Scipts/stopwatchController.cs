using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class stopWatchController : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] TextMeshProUGUI stopwatchText;
    private float elapsedTime;
    private bool isRunning = false;
    private bool isPaused = false;

    private RectTransform stopwatchRect;
    private Vector2 stemAreaCenter = new Vector2(0.5f, 0.15f);
    private float stemAreaRadius = 0.1f;

    void Start() {
        stopwatchRect = GetComponent<RectTransform>();
    }


    // Update is called once per frame
    void Update()
    {
        if (isRunning && !isPaused)
        {
            UpdateTime();
        }
    }

    //Handles Clicks
    public void OnPointerDown(PointerEventData eventData) {
        // normalize pointer position & then normalize the coordinates
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(stopwatchRect, eventData.position, eventData.enterEventCamera, out localPoint);
        Vector2 normalizedPoint = new Vector2((localPoint.x / stopwatchRect.rect.width) + 0.5f, (localPoint.y / stopwatchRect.rect.height) + 0.5f);

        if (Vector2.Distance(normalizedPoint, stemAreaCenter) <= stemAreaRadius) // click in stem area, reset timer
        {
            ResetStopWatch();
        }
        else // click in the rest of the stopwatch area
        { 
            if (isRunning)
            {
                PauseStopWatch();
            }
            else {
                StartStopWatch();
            }
        }
    }

    // Start or Resume the Timer
    public void StartStopWatch()
    {
        if (!isRunning)
        {
            isRunning = true;
        }
        isPaused = false;
    }

    // Pause the Timer
    public void PauseStopWatch()
    {
        if (isRunning)
        {
            isPaused = true;
            isRunning = false;
        }
    }

    // Reset the Timer
    public void ResetStopWatch()
    {
        elapsedTime = 0;
        isRunning = false;
        isPaused = true;
        UpdateTime();
    }

    public void UpdateTime()
    {
        elapsedTime += Time.deltaTime; // increase by elapsed time by 1 every second
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        // Format & display the time
        stopwatchText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}