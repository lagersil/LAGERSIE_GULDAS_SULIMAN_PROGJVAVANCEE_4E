using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public string levelToLoad;
    public GameObject settingsWindow;
    public GameObject choix;

    public void StartGame_IA()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void StartGame_MCTS()
    {
        SceneManager.LoadScene("");
    }

    public void StartGame_VS()
    {
        SceneManager.LoadScene("1v1");
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
