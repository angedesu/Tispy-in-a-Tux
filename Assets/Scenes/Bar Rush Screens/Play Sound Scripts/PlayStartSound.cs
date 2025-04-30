using UnityEngine;
using UnityEngine.UI;

public class PlayStartSound : MonoBehaviour
{
    public void PlaySound()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.startSound);
    }
}