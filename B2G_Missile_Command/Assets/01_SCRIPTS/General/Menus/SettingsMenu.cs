using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class SettingsMenu : MonoBehaviour
{
    public GameObject GeneralPanel;
    public GameObject SettingsPanel;
    public GameObject ControlPanel;


    public SpriteRenderer Sound;

    public AudioMixer SoundMixer, MusicMixer;

    public Dropdown resolutionDropdown;

    public GameObject lePlayButton, leSettingsButton, leReturnButton, leReturnButton2;
    public GameObject SliderSound, SliderMusic;

    Resolution[] resolutions;

    ControlSettings settingManager;
    public GameObject[] Buttons;

    private void Start()
    {
        settingManager = GameObject.Find("ControlManager").GetComponent<ControlSettings>();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(lePlayButton);

        resolutions = Screen.resolutions;

        //resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        //Display the list of disponible resolution
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            //Verify the selected current resolution
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        //resolutionDropdown.AddOptions(options);
        //resolutionDropdown.value = currentResolutionIndex;
        //resolutionDropdown.RefreshShownValue();
    }

    void Update()
    {
        Buttons[0].GetComponentInChildren<Text>().text = settingManager.Shoot.GetBindingDisplayString(0);
        Buttons[1].GetComponentInChildren<Text>().text = settingManager.SwitchRight.GetBindingDisplayString(0);
        Buttons[2].GetComponentInChildren<Text>().text = settingManager.SwitchLeft.GetBindingDisplayString(0);

        if(Gamepad.current.rightShoulder.wasPressedThisFrame && SettingsPanel.activeSelf)
        {
            SettingsPanel.gameObject.SetActive(false);
            ControlPanel.gameObject.SetActive(true);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(leReturnButton2);
        }
        if(Gamepad.current.leftShoulder.wasPressedThisFrame && ControlPanel.activeSelf)
        {
            SettingsPanel.gameObject.SetActive(true);
            ControlPanel.gameObject.SetActive(false);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(leReturnButton);
        }
    }



    //Update the current resolution
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    
    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    //General Menu Command
    public void PlayButton()
    {
        //Load Scene number 1 in the build
        SceneManager.LoadScene(1, LoadSceneMode.Single);

    }

    public void SettingsButton()
    {
        GeneralPanel.gameObject.SetActive(false);
        SettingsPanel.gameObject.SetActive(true);
        SliderSound.GetComponent<Slider>().value = GetMixerValue(SoundMixer);
        SliderMusic.GetComponent<Slider>().value = GetMixerValue(MusicMixer);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(leReturnButton);
    }

    public void ControlButton()
    {
        GeneralPanel.gameObject.SetActive(false);
        ControlPanel.gameObject.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(leReturnButton2);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    //Settings Menu Command

    //Call MainAudioMixer//
    public void SetVolumeSound(float volume)
    {
        //Caution don't touch VolumeSlider Min and Max Value//
        SoundMixer.SetFloat("volume", volume);
    }

    public void SetVolumeMusic(float volume)
    {
        //Caution don't touch VolumeSlider Min and Max Value//
        MusicMixer.SetFloat("volume", volume);
    }

    public void ReturnButton()
    {
        SettingsPanel.gameObject.SetActive(false);
        ControlPanel.gameObject.SetActive(false);
        GeneralPanel.gameObject.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(lePlayButton);
    }
    public void MenuButton()
    {
        GeneralPanel.gameObject.SetActive(true);
        SettingsPanel.gameObject.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(leReturnButton);

    }

    public void SoundOnOff()
    {
        bool active = true;


    }

    float GetMixerValue(AudioMixer mixer)
    {
        float value;
        bool result = mixer.GetFloat("volume", out value);
        if (result)
        {
            return value;
        }
        else
        {
            return 0;
        }
    }

    public void RemapShoot()
    {
        settingManager.Shoot.Disable();

        var rebindOperation = settingManager.Shoot.PerformInteractiveRebinding()
                    // To avoid accidental input from mouse motion
                    .WithControlsExcluding("Mouse")
                    .OnMatchWaitForAnother(0.1f)
                    .Start();
        settingManager.Shoot.Enable();
    }

    public void RemapSwitchRight()
    {
        settingManager.SwitchRight.Disable();

        var rebindOperation = settingManager.SwitchRight.PerformInteractiveRebinding()
                    // To avoid accidental input from mouse motion
                    .WithControlsExcluding("Mouse")
                    .OnMatchWaitForAnother(0.1f)
                    .Start();
        settingManager.SwitchRight.Enable();
    }

    public void RemapSwitchLeft()
    {
        settingManager.SwitchLeft.Disable();

        var rebindOperation = settingManager.SwitchLeft.PerformInteractiveRebinding()
                    // To avoid accidental input from mouse motion
                    .WithControlsExcluding("Mouse")
                    .OnMatchWaitForAnother(0.1f)
                    .Start();
        settingManager.SwitchLeft.Enable();
    }
}
