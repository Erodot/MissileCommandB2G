using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public GameObject GeneralPanel;
    public GameObject SettingsPanel;

    public AudioMixer audioMixer;

    //General Menu Command
    public void PlayButton()
    {
        //SceneManager.LoadScene("Name Your Scene here", LoadSceneMode.Single);
    }

    public void SettingsButton()
    {
        GeneralPanel.gameObject.SetActive(false);
        SettingsPanel.gameObject.SetActive(true);
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
    }
}
