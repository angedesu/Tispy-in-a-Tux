using UnityEngine;
using UnityEngine.UI;

public class PlayDoorSound: MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(PlaySound);
    }

    private void PlaySound()
    {   
        // Run the PlaySFX function from the Audio Manager, Replace "doorOpenSound" with the sound you want
        AudioManager.instance.PlaySFX(AudioManager.instance.doorOpenSound);
    }

}