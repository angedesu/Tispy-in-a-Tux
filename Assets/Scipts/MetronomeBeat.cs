using UnityEditor.TerrainTools;
using UnityEngine;

public class MetronomeBeat : MonoBehaviour
{
    private int bpm;
    private int bpmInSeconds;
    private double nextTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int bpm = 200;
        bpmInSeconds = 60 / bpm;
        nextTime = AudioSettings.dspTime + bpmInSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        if (AudioSettings.dspTime >= nextTime) {
            Debug.Log("Tick Tock");
            GetComponent<AudioSource>().Play();
        }
    }
}
