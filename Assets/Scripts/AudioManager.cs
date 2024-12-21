using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    public AudioSource vfxAudioSource;
    public AudioSource musicAudioSource;

    [Header("Audio Clip")]
    public AudioClip musicClip;
    public AudioClip bossMusicClip;
    public AudioClip CherriesClip;
    public AudioClip jumpClip;
    public AudioClip hitClip;
    public AudioClip shurikenClip;
    public AudioClip dashClip;
    public AudioClip takeDameClip;

    private void Start()
    {
        if (vfxAudioSource == null || musicAudioSource == null)
        {
            Debug.LogError("Audio sources not assigned in AudioManager.");
        }

        if (GameObject.Find("LevelBoss"))
        {
            musicAudioSource.clip = bossMusicClip;
        }
        else
        {
            musicAudioSource.clip = musicClip;
        }
        musicAudioSource.Play();
    }

    public void PlaySFX(AudioClip sfxClip)
    {
        if (vfxAudioSource != null && sfxClip != null)
        {
            vfxAudioSource.PlayOneShot(sfxClip);
        }
    }
}