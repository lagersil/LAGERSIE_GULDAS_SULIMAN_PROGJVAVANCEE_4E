using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLose : MonoBehaviour
{
    // Start is called before the first frame update
    public void Replay()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
