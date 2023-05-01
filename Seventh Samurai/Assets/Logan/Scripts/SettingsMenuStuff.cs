using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class SettingsMenuStuff : MonoBehaviour
{
    [Header("Resolution")]
    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;

    [Header("CamSense")]
    public static float cameraSensitivity;
    public CinemachineFreeLook playerCam;

    [Header("Volume")]
    public AudioMixer audioMixer;
    public GameObject settingsMenuGameObject;
    private float mastVol = -20;
    private float sfxVol = 0;
    private float enviroVol = 0;
    private float musicVol = 0;

    public Slider mastVolSlider;
    public Slider musicVolSlider;
    public Slider sfxVolSlider;
    public Slider enviroVolSlider;
    public Slider camSensSlider;
    public float camSens;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();


        mastVol = PlayerPrefs.GetFloat("mastVolPref", 50);
        sfxVol =  PlayerPrefs.GetFloat("sfxVolPref", 50);
        enviroVol = PlayerPrefs.GetFloat("enviroVolPref", 50);
        musicVol = PlayerPrefs.GetFloat("musicVolPref", 50);
        camSens = PlayerPrefs.GetFloat("camSensPref", 450);

        mastVolSlider.value = mastVol;
        musicVolSlider.value = musicVol;
        sfxVolSlider.value = sfxVol;
        enviroVolSlider.value = enviroVol;
        camSensSlider.value = camSens;

        if (playerCam != null)
        {
            playerCam.m_XAxis.m_MaxSpeed = camSens;
        }

        //settingsMenuGameObject.SetActive(false);

    }



    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    //Master Volume
    public void SetMastVolume(float volume)
    {
        if(volume < 1)
        {
            volume = .001f;
        }

        RefreshMastSlider(volume);
        PlayerPrefs.SetFloat("mastVolPref", volume);
        audioMixer.SetFloat("masterVolParam", Mathf.Log10(volume/100) * 20);
    }
    public void SetMastVolumeFromSlider()
    {
        SetMastVolume(mastVolSlider.value);
    }
    public void RefreshMastSlider(float volume)
    {
        mastVolSlider.value = volume;
    }

    //Music Volume
    public void SetMusicVolume(float volume)
    {
        if (volume < 1)
        {
            volume = .001f;
        }

        RefreshMusicSlider(volume);
        PlayerPrefs.SetFloat("musicVolPref", volume);
        audioMixer.SetFloat("musicVolParam", Mathf.Log10(volume / 100) * 20);
    }
    public void SetMusicVolumeFromSlider()
    {
        SetMusicVolume(musicVolSlider.value);
    }
    public void RefreshMusicSlider(float volume)
    {
        musicVolSlider.value = volume;
    }

    //Environment Volume
    public void SetEnviroVolume(float volume)
    {
        if (volume < 1)
        {
            volume = .001f;
        }

        RefreshEnvSlider(volume);
        PlayerPrefs.SetFloat("enviroVolPref", volume);
        audioMixer.SetFloat("enviroVolParam", Mathf.Log10(volume / 100) * 20);
    }
    public void SetEnvVolumeFromSlider()
    {
        SetEnviroVolume(enviroVolSlider.value);
    }
    public void RefreshEnvSlider(float volume)
    {
        enviroVolSlider.value = volume;
    }

    //SFX Volume
    public void SetSFXVolume(float volume)
    {
        if (volume < 1)
        {
            volume = .001f;
        }

        RefreshSFXSlider(volume);
        PlayerPrefs.SetFloat("sfxVolPref", volume);
        audioMixer.SetFloat("sfxVolParam", Mathf.Log10(volume / 100) * 20);
    }
    public void SetSFXVolumeFromSlider()
    {
        SetSFXVolume(sfxVolSlider.value);
    }
    public void RefreshSFXSlider(float volume)
    {
        sfxVolSlider.value = volume;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetCamSens(float sensitivity)
    {
        camSens = sensitivity;
        playerCam.m_XAxis.m_MaxSpeed = camSens;
        PlayerPrefs.SetFloat("camSensPref", sensitivity);
    }

}
