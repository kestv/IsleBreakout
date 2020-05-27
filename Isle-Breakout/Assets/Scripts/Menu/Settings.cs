using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField]AudioMixer mixer;
    [SerializeField]GameObject menu;
    [SerializeField]GameObject options;
    [SerializeField]Dropdown resolutionDropdown;
    [SerializeField] Dropdown fullscreenDropdown;

    Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            if(resolutions[i].width >= 1200)
                options.Add(option);

            if(resolutions[i].width == Screen.width &&
                resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        SetResolution(resolutions.Length - 1);
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = resolutions.Length - 1;
        resolutionDropdown.RefreshShownValue();

        fullscreenDropdown.value = Screen.fullScreen ? 0 : 1;
        fullscreenDropdown.RefreshShownValue();

        
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetVolume(float volume)
    {
        mixer.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(int fullscreen)
    {
        if (fullscreen == 1)
            Screen.fullScreen = false;
        else 
            Screen.fullScreen = true;
    }

    public void BackToMenu()
    {
        menu.SetActive(true);
        options.SetActive(false);
    }
}
