using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Audio;

//Classe permettant de gerer le menu de parametre
public class SettingMenu : MonoBehaviour
{
    public TMPro.TMP_Dropdown resolutionDropdown;
    public AudioMixer audioMixer;

    Resolution[] resolutions;

    /* 
    * Cette fonction est appelee au demarrage du script ou de l'objet auquel il est attache dans Unity.
    * Elle initialise la fenetre de jeu a une certaine resolution.
    */
    public void Start()
    {
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        Screen.fullScreen = true;
    }

    //Regle le volume du jeu
    public void setVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    //Switch entre le pleine ecran et la fenetre
    public void setFullScreen(bool isfullscreen)
    {
        Screen.fullScreen = isfullscreen;
    }

    //Regle la resolution de l'ecran
    public void setResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
