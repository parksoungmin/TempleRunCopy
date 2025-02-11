using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager: MonoBehaviour
{
    public AudioMixer audioMixer;

    private void Start()
    {
        audioMixer.SetFloat("SFX_Volume", Mathf.Log10(SaveLoadManager.Data.sfxSound)*20);
        audioMixer.SetFloat("BGM_Volume", Mathf.Log10(SaveLoadManager.Data.bgmSound)*20);
    }

    public void SFXVolumSet(float vol)
    {
        audioMixer.SetFloat("SFX_Volume", Mathf.Log10(vol)*20);
        SaveLoadManager.Data.sfxSound = vol;
        SaveLoadManager.Save();
    }
    public void BGMVolumSet(float vol)
    {
        audioMixer.SetFloat("BGM_Volume", Mathf.Log10(vol)*20);
        SaveLoadManager.Data.bgmSound = vol;
        SaveLoadManager.Save();
    }
}
