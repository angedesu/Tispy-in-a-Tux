using UnityEngine;
using UnityEngine.UI;

public class PlayServedSound : MonoBehaviour
{
    public void PlaySound()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.servedSound);
    }
}