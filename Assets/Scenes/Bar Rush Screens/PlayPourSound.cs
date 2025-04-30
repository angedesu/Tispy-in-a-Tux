using UnityEngine;
using UnityEngine.UI;

public class PlayPourSound : MonoBehaviour
{
    public void PlaySound()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.pourSound);
    }
}