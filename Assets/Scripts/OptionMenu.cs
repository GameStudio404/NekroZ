using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour {
    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;

    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentRes = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
                currentRes = i;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentRes;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetVolume(float volume) {
        if (volume < -40.0)
        {
            volume = (float) -80.0;
        }
        audioMixer.SetFloat("MainVolume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resIndex)
    {
        Resolution res = resolutions[resIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
}
