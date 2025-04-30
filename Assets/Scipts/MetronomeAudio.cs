using System.Collections;
using UnityEngine;

public class MetronomeAudio : MonoBehaviour
{
    public AudioSource metronomeSource;
    private float bpm = 60f;
    private float interval;
    private bool isMetronomeMute;

    // Private Functions
    //=======================================================================================
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        isMetronomeMute = true;
        interval = 60f / bpm;
    }

    /*
    // Update is called once per frame
    private void Update()
    {
    }
    */

    private void PlayMetronomeClick()
    {
        metronomeSource.Play();
        StartCoroutine(StopClickAfterShortDelay());
    }

    private IEnumerator StopClickAfterShortDelay()
    {
        yield return new WaitForSeconds(0.2f); // stop after 0.2 seconds
        metronomeSource.Stop();
    }
    //=======================================================================================


    // Public Functions
    //=======================================================================================
    public void MetronomeMuteUnmute()
    {
        if (isMetronomeMute) // Metronome is muted
        {
            MuteAllExceptMetronome(); // Metronome is unmuted, everything else is muted
            InvokeRepeating(nameof(PlayMetronomeClick), 0f, interval);
            isMetronomeMute = false; // Metronome is unmuted
        }
        else
        {
            UnmuteAll(); // Metronome is muted, everything else is unmuted
            CancelInvoke(nameof(PlayMetronomeClick));
            isMetronomeMute = true; // Metronome is muted
        }
    }

    public void MuteAllExceptMetronome()
    {
        AudioSource[] allSources = Object.FindObjectsByType<AudioSource>(FindObjectsSortMode.None);

        foreach (AudioSource src in allSources)
        {
            if (src != metronomeSource)
                src.mute = true; // mute everything but the metronome
            else
                src.mute = false; // unmute only the metronome
        }
    }

    public void UnmuteAll()
    {
        AudioSource[] allSources = Object.FindObjectsByType<AudioSource>(FindObjectsSortMode.None);

        foreach (AudioSource src in allSources)
        {
            if (src == metronomeSource)
                src.mute = true; // mute only the metronome
            else
                src.mute = false; // unmute everything but the metronome
        }
    }
    //=======================================================================================
}

