using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;

    void Awake()
    {
        Time.timeScale = 1;    
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseButtonClick();
        }
    }

    public void PauseButtonClick(){
        AudioManager.Instance.PlaySFX2("button");
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        pauseScreen.SetActive(true);
    }

    public void ContinueButtonClick(){
        AudioManager.Instance.PlaySFX2("button");
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}
