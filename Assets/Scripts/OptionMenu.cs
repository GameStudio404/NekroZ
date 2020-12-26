using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionMenu : MonoBehaviour {
    public AudioMixer audioMixer;

    public void SetVolume(float volume) {
        if (volume < -40.0)
        {
            volume = (float) -80.0;
        }
        audioMixer.SetFloat("MainVolume", volume);
    }
}
