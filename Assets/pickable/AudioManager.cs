using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioClip coin, power;
    public AudioSource AudioSource;

    private void Awake()
    {
        Instance = this;
    }

    public void PlaySFX(string name)
    {
        if(name == "coin"){
            AudioSource.PlayOneShot(coin);
        }
        else if(name == "power"){
            AudioSource.PlayOneShot(power);
        }
    }
}
