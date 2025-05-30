using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;
    public float timeRemaining;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
            timeRemaining -= Time.deltaTime;
    }
    
    public void StartTimer(float seconds)
    {
        timeRemaining = seconds;
        // Debug.Log($"Timer started with {seconds} seconds");
    }
}
