using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager: MonoBehaviour
{
    public AudioMixer audioMixer;

    private void Start()
    {
        SoundOptionSet();
    }

    public void SFXVolumSet(float vol)
    {
        SaveLoadManager.Data.sfxSound = vol;
        audioMixer.SetFloat("SFX_Volume", Mathf.Log10(vol)*20);
        SaveLoadManager.Save();
    }
    public void BGMVolumSet(float vol)
    {
        SaveLoadManager.Data.bgmSound = vol;
        audioMixer.SetFloat("BGM_Volume", Mathf.Log10(vol)*20);
        SaveLoadManager.Save();
    }
    public void SoundOptionSet()
    {
        audioMixer.SetFloat("SFX_Volume", Mathf.Log10(SaveLoadManager.Data.sfxSound) * 20);
        audioMixer.SetFloat("BGM_Volume", Mathf.Log10(SaveLoadManager.Data.bgmSound) * 20);
    }
}
