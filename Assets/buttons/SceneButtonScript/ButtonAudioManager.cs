using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonAudioManager : MonoBehaviour
{
    public static ButtonAudioManager Instance;

    public void Awake()
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

    public void buttonAudio()
    {
        AudioManager.Instance.PlaySFX2("button");

    }

    public void retryScreenAudio()
    {
        AudioManager.Instance.PlayMusic("bgm");
    }

    public void exitScreenAudio()
    {
        AudioManager.Instance.PlayMusic("intro");
    }

    public void loseScreenAudio()
    {
        AudioManager.Instance.PlayMusic("lose");
    }

    public void winScreenAudio()
    {
        AudioManager.Instance.PlayMusic("win");
    }
}
