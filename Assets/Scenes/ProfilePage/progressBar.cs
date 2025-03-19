using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Slider slider;
    public float fillSpeed = 0.5f;

    private void Start()
    {
        slider = GetComponent<Slider>();
        PlayerPrefs.SetFloat("Progress", 0.5f);
        // check current progress
        CheckProgress();
    }

    void CheckProgress()
    {
        float currentProgress = PlayerPrefs.GetFloat("xpProgress", 0);
        if (currentProgress > 0)
        {
            IncrementProgress(currentProgress);
        }
    }

    void UpdateProgress()
    {
        PlayerPrefs.SetFloat("xpProgress", slider.value);
        PlayerPrefs.Save();
    }
    public void IncrementProgress(float progress)
    {
        slider.value += progress;
        if (slider.value < 1)
        {
            slider.value += fillSpeed * Time.deltaTime;
        }
        UpdateProgress();
    }
}
