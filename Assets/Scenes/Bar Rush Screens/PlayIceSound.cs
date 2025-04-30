using UnityEngine;
using UnityEngine.UI;

public class PlayIceSound : MonoBehaviour
{
    public void PlaySound()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.iceSound);
    }
}
