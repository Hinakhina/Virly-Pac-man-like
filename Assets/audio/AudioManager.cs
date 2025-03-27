using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds1, sfxSounds2, loopSounds;
    public AudioSource musicSource, sfxSource1, sfxSource2, loopSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        PlayMusic("intro");

    }

    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicSounds, x=> x.name == name);

        if (sound != null)
        {
            musicSource.clip = sound.clip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
            musicSource.Stop();
    }

    public void PlaySFX1(string name)
    {
        Sound sound = Array.Find(sfxSounds1, x=> x.name == name);

        if (sound != null)
        {
            sfxSource1.PlayOneShot(sound.clip);
        }

    }

    public void PlaySFX2(string name)
    {
        Sound sound = Array.Find(sfxSounds2, x=> x.name == name);

        if (sound != null)
        {
            sfxSource2.PlayOneShot(sound.clip);
        }
    }

    public void PlayLoop(string name)
    {
        Sound sound = Array.Find(loopSounds, x=> x.name == name);

        if (sound != null)
        {
            loopSource.clip = sound.clip;
            loopSource.Play();
        }
    }
}

