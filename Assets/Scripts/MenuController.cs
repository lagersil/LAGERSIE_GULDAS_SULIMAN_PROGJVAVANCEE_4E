using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public string levelToLoad;
    public GameObject settingsWindow;
    public GameObject choix;
    public bool choixIa; 
    public bool choixPlayer; 
    public bool choixMCTS;

    // Methode pour commencer une partie contre l'IA 
    public void StartGame_IA()
    {
      
        PlayerPrefs.SetInt("IA", 1);
        PlayerPrefs.SetInt("MCTS", 0);
        PlayerPrefs.SetInt("Humain", 0);
        SceneManager.LoadScene("SampleScene");
    }

    // Méthode pour commencer une partie avec MCTS
    public void StartGame_MCTS()
    { PlayerPrefs.SetInt("IA", 0);
        PlayerPrefs.SetInt("MCTS", 1);
        PlayerPrefs.SetInt("Humain", 0);
        SceneManager.LoadScene("SampleScene");
    }

    // Méthode pour commencer une partie en mode Joueur vs. Joueur humain
    public void StartGame_VS()
    {
        PlayerPrefs.SetInt("IA", 0);
        PlayerPrefs.SetInt("MCTS", 0);
        PlayerPrefs.SetInt("Humain", 1);
        SceneManager.LoadScene("SampleScene");
    }

    // Méthode pour afficher la fenêtre de choix
    public void choixGame()
    {
        choix.SetActive(true);
    }

    // Méthode pour afficher la fenêtre de paramètres
    public void SettingsButton()
    {
        settingsWindow.SetActive(true);
    }

    // Méthode pour fermer la fenêtre de paramètres
    public void CloseSettingsWindow()
    {
        settingsWindow.SetActive(false);
    }

    // Méthode pour quitter l'application
    public void QuitGame()
    {
        Application.Quit();
    }
}
