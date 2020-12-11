using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    ControlSettings settingManager;

    public bool paused;
    bool canPause = true;

    public GameObject pauseMenu;
    public GameObject pauseFirstButton;

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
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
                gameManager.turretCanShoot = true;
            }
            else
            {
                paused = true;
                pauseMenu.SetActive(true);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(pauseFirstButton);
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
        Buttons[4].GetComponentInChildren<Text>().text = settingManager.Pause.GetBindingDisplayString(0);

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
}
