using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUi : MonoBehaviour
{
    public SoundManager SoundManager;

    public Slider sfxSlider;
    public Slider bgmSlider;
    void Start()
    {
        SoundManager.SoundOptionSet();
        sfxSlider.value = SaveLoadManager.Data.sfxSound;
        bgmSlider.value = SaveLoadManager.Data.bgmSound;
    }
}
