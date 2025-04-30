using UnityEngine;
using UnityEngine.UI;

public class PlayMixSound : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(PlaySound);
    }
    
    public void PlaySound()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.mixSound);
    }
}