using UnityEngine;
using UnityEngine.UI;

public class PlayYuckSound : MonoBehaviour
{
    public void PlaySound()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.yuckSound);
    }
}