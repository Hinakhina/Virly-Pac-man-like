using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KilledEnemyButton : MonoBehaviour
{
    [SerializeField] GameObject killedEnemyScreen;
    [SerializeField] GameObject PauseButton;

    public void ScreenActive(){
        Debug.Log("Opening Panel");
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        killedEnemyScreen.SetActive(true);
        PauseButton.SetActive(false);
    }

    public void ContinueButtonClick(){
        AudioManager.Instance.PlaySFX2("button");
        Time.timeScale = 1;
        killedEnemyScreen.SetActive(false);
        PauseButton.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}
