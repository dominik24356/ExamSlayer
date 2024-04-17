using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource; 
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip mainTheme;
    public AudioClip death;
    public AudioClip destruction;
    public AudioClip respawn;
    public AudioClip shoot;
    public AudioClip reload;
    public AudioClip buff;
    public AudioClip speed_boost;
    public AudioClip reload_start;

    private void Start()
    {
        musicSource.clip = mainTheme;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
