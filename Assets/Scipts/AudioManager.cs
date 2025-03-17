using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    
    [Header("Audio Clip")]
    public AudioClip backgroundMusic;

    private void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }
}
