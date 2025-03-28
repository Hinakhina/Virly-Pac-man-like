using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void retryScreen()
    {
        ButtonAudioManager.Instance.buttonAudio();
        SceneManager.LoadScene("AlchemistLevel");
        ButtonAudioManager.Instance.retryScreenAudio();
    }

    public void exitScreen()
    {
        ButtonAudioManager.Instance.buttonAudio();
        SceneManager.LoadScene("MainMenu");
        AudioManager.Instance.PlayMusic("intro");
    }


    public void exitGame()
    {
        ButtonAudioManager.Instance.buttonAudio();
        Application.Quit();
    }

    public void loseScreen()
    {
        SceneManager.LoadScene("LoseScene");
        ButtonAudioManager.Instance.loseScreenAudio();
    }

    public void winScreen()
    {
        SceneManager.LoadScene("WinScene");
        ButtonAudioManager.Instance.winScreenAudio();
    }

    public void hoverButton()
    {
        ButtonAudioManager.Instance.hoverButtonAudio();
    }
}
