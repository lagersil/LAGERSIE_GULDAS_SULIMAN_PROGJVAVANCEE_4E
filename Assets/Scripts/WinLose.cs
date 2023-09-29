using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLose : MonoBehaviour
{
    public GameState gameState;
    public GameObject PanelLose;
    public GameObject PanelWin;

    public WinLose(GameState game)
    {
        this.gameState = game;
    }
    
    public void Score()
    {
        if(gameState.victoireJoueur == true)
        {
            PanelWin.SetActive(true);
        }
        else
        {
            PanelLose.SetActive(true);
        }
    }
    public void Replay()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
