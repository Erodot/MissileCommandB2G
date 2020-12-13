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

    public AudioMixer audioMixer;

    public Dropdown resolutionDropdown;

    public GameObject lePlayButton, leSettingsButton, leReturnButton;

    Resolution[] resolutions;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(lePlayButton);

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

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

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
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

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(leReturnButton);

    }

    public void QuitButton()
    {
        Application.Quit();
    }

    //Settings Menu Command

    //Call MainAudioMixer//
    public void SetVolume(float volume)
    {
        //Caution don't touch VolumeSlider Min and Max Value//
        audioMixer.SetFloat("volume", volume);
    }

    public void ReturnButton()
    {
        SettingsPanel.gameObject.SetActive(false);
        GeneralPanel.gameObject.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(leSettingsButton);
    }
}
