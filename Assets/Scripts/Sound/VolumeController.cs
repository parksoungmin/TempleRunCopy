using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    public AudioMixer audioMixer; // AudioMixer ����
    public Slider bgmSlider; // BGM �����̴�
    public Slider sfxSlider; // SFX �����̴�

    private void Start()
    {
        // �����̴� ���� ����� �� ���� ����
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        // ����� ���� �� �ҷ�����
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
        // ����� ���� �ִٸ� �ҷ�����, ������ 0.75 �⺻��
        float bgmValue = PlayerPrefs.GetFloat("BGM_Volume", 0.75f);
        float sfxValue = PlayerPrefs.GetFloat("SFX_Volume", 0.75f);

        bgmSlider.value = bgmValue;
        sfxSlider.value = sfxValue;

        // �ҷ��� �� ����
        SetBGMVolume(bgmValue);
        SetSFXVolume(sfxValue);
    }
}