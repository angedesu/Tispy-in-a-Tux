using UnityEngine;
using UnityEngine.UI;

public class PlayTrashSound : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(PlaySound);
    }
    
    public void PlaySound()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.trashCanSound);
    }
}

