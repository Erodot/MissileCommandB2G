using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    ControlSettings settingManager;

    public bool paused;
    bool canPause = true;

    public GameObject PausePanel;

    public GameObject PauseMenu;
    public GameObject Options;
    public GameObject Back;
    public GameObject Sound;
    public GameObject Control;

    public GameObject PauseMenuButton;
    public GameObject OptionsButton;
    public GameObject BackButton;
    public GameObject SoundButtton;
    public GameObject ControlButton;
    public GameObject CurrentPanel;

    public GameObject[] Buttons;

    public GameManager gameManager;

    void Start()
    {
        settingManager = GameObject.Find("ControlManager").GetComponent<ControlSettings>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.RoundToInt(settingManager.Pause.ReadValue<float>()) == 1 && canPause == true)
        {
            canPause = false;
            if (paused)
            {
                paused = false;
                CurrentPanel.SetActive(false);
                PausePanel.SetActive(false);
                Time.timeScale = 1;
                gameManager.turretCanShoot = true;
            }
            else
            {
                paused = true;
                PausePanel.SetActive(true);
                PauseMenu.SetActive(true);
                CurrentPanel = PauseMenu;
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(PauseMenuButton);
                Time.timeScale = 0;
                gameManager.turretCanShoot = false;
            }

        }
        else if (Mathf.RoundToInt(settingManager.Pause.ReadValue<float>()) != 1 && canPause == false)
        {
            canPause = true;
        }

        Buttons[0].GetComponentInChildren<Text>().text = settingManager.Shoot.GetBindingDisplayString(0);
        Buttons[1].GetComponentInChildren<Text>().text = settingManager.SwitchRight.GetBindingDisplayString(0);
        Buttons[2].GetComponentInChildren<Text>().text = settingManager.SwitchLeft.GetBindingDisplayString(0);

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

    public void RemapPause()
    {
        settingManager.Pause.Disable();

        var rebindOperation = settingManager.Pause.PerformInteractiveRebinding()
                    // To avoid accidental input from mouse motion
                    .WithControlsExcluding("Mouse")
                    .OnMatchWaitForAnother(0.1f)
                    .Start();
        settingManager.Pause.Enable();
    }

    public void Resume()
    {
        if (paused)
        {
            paused = false;
            CurrentPanel.SetActive(false);
            PausePanel.SetActive(false);
            Time.timeScale = 1;
            gameManager.turretCanShoot = true;
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadOptions()
    {
        CurrentPanel.SetActive(false);
        Options.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(OptionsButton);
        CurrentPanel = Options;
    }

    public void LoadControl()
    {
        CurrentPanel.SetActive(false);
        Control.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(ControlButton);
        CurrentPanel = Control;
    }

    public void LoadSound()
    {
        CurrentPanel.SetActive(false);
        Sound.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(SoundButtton);
        CurrentPanel = Sound;
    }

    public void LoadBack()
    {
        CurrentPanel.SetActive(false);
        Back.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(BackButton);
        CurrentPanel = Back;
    }

    public void LoadPause()
    {
        CurrentPanel.SetActive(false);
        PauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(PauseMenuButton);
        CurrentPanel = PauseMenu;
    }
}
