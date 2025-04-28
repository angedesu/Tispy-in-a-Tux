using UnityEngine;
using TMPro;

public class countdown : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] float remainingTime;

    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0) //remaining time must be positive to decrease
        { 
            remainingTime -= Time.deltaTime; //decrease remaining time 
        }
        else if (remainingTime < 0) {  //if remaining time is negative, make it zero
            remainingTime = 0; 
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}