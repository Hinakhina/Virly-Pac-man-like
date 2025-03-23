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
        SceneManager.LoadScene("GamePlay");
    }

    public void exitScreen()
    {
        SceneManager.LoadScene("MainMenu");
    }


    public void exitGame()
    {
        Application.Quit();
    }
}
