using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class SettingsMenuStuff : MonoBehaviour
{

    public AudioMixer audioMixer;
    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;
    public static float cameraSensitivity;

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


    public CinemachineFreeLook playerCam;

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

        mastVol = PlayerPrefs.GetFloat("mastVolPref");
        sfxVol =  PlayerPrefs.GetFloat("sfxVolPref");
        enviroVol = PlayerPrefs.GetFloat("enviroVolPref");
        musicVol = PlayerPrefs.GetFloat("musicVolPref");
        camSens = PlayerPrefs.GetFloat("camSensPref");

        mastVolSlider.value = mastVol;
        musicVolSlider.value = musicVol;
        sfxVolSlider.value = sfxVol;
        enviroVolSlider.value = enviroVol;
        camSensSlider.value = camSens;


        if(playerCam != null)
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

    public void SetMastVolume(float volume)
    {
        audioMixer.SetFloat("masterVolParam", volume);
        PlayerPrefs.SetFloat("mastVolPref", volume);
        
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("sfxVolParam", volume);
        PlayerPrefs.SetFloat("sfxVolPref", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolParam", volume);
        PlayerPrefs.SetFloat("musicVolPref", volume);
    }

    public void SetEnviroVolume(float volume)
    {
        audioMixer.SetFloat("enviroVolParam", volume);
        PlayerPrefs.SetFloat("enviroVolPref", volume);
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
