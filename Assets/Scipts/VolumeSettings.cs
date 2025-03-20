using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider masterSlider;

    private void Start()
    {   
        if (PlayerPrefs.HasKey("userMusicVolume") || PlayerPrefs.HasKey("userSfxVolume") || PlayerPrefs.HasKey("userMasterVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSfxVolume();
            SetMasterVolume();
        }
    }
    
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        // Audio mixer volume change logarithmically, Slider value change linearly
        audioMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20);
        // Save the volume setting
        PlayerPrefs.SetFloat("userMusicVolume", volume);
    }

    public void SetSfxVolume()
    {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("sfxVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("userSfxVolume", volume);
    }

    public void SetMasterVolume()
    {
        float volume = masterSlider.value;
        audioMixer.SetFloat("masterVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("userMasterVolume", volume);
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("userMusicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("userSfxVolume");
        masterSlider.value = PlayerPrefs.GetFloat("userMasterVolume");
        
        SetMusicVolume();
        SetSfxVolume();
        SetMasterVolume();
    }
}
