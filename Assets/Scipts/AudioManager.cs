using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    
    [Header("Audio Clip")]
    public AudioClip backgroundMusic;
    public AudioClip doorOpenSound;
    public AudioClip buttonClickSound;
    public AudioClip BookOpenSound;
    public AudioClip ToolsTouchingSound;
    
    
    // Audio manager is a singleton (only 1 instance throughout scenes)
    public static AudioManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic(backgroundMusic);
    }
    
    // Play background music
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }
    
    // Play SFX
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.PlayOneShot(clip);
    }
    
    
}
