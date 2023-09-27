using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public string levelToLoad;
    public GameObject settingsWindow;
    public GameObject choix;

    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void choixGame()
    {
        choix.SetActive(true);
    }

    public void SettingsButton()
    {
        settingsWindow.SetActive(true);
    }

    public void CloseSettingsWindow()
    { 
        settingsWindow.SetActive(false); 
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
