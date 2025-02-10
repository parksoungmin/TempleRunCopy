using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    public AudioMixer audioMixer; // AudioMixer 연결
    public Slider bgmSlider; // BGM 슬라이더
    public Slider sfxSlider; // SFX 슬라이더

    private void Start()
    {
        // 슬라이더 값이 변경될 때 볼륨 조절
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        // 저장된 볼륨 값 불러오기
        LoadVolume();
    }

    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGM_Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("BGM_Volume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX_Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFX_Volume", volume);
    }

    private void LoadVolume()
    {
        // 저장된 값이 있다면 불러오기, 없으면 0.75 기본값
        float bgmValue = PlayerPrefs.GetFloat("BGM_Volume", 0.75f);
        float sfxValue = PlayerPrefs.GetFloat("SFX_Volume", 0.75f);

        bgmSlider.value = bgmValue;
        sfxSlider.value = sfxValue;

        // 불러온 값 적용
        SetBGMVolume(bgmValue);
        SetSFXVolume(sfxValue);
    }
}