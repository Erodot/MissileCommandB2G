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
        Buttons[1].GetComponentInChildren<Text>().text = settingManager.Turret1.GetBindingDisplayString(0);
        Buttons[2].GetComponentInChildren<Text>().text = settingManager.Turret2.GetBindingDisplayString(0);
        Buttons[3].GetComponentInChildren<Text>().text = settingManager.Turret3.GetBindingDisplayString(0);
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

    public void RemapTurret1()
    {
        settingManager.Turret1.Disable();

        var rebindOperation = settingManager.Turret1.PerformInteractiveRebinding()
                    // To avoid accidental input from mouse motion
                    .WithControlsExcluding("Mouse")
                    .OnMatchWaitForAnother(0.1f)
                    .Start();
        settingManager.Turret1.Enable();
    }

    public void RemapTurret2()
    {
        settingManager.Turret2.Disable();

        var rebindOperation = settingManager.Turret2.PerformInteractiveRebinding()
                    // To avoid accidental input from mouse motion
                    .WithControlsExcluding("Mouse")
                    .OnMatchWaitForAnother(0.1f)
                    .Start();
        settingManager.Turret2.Enable();
    }

    public void RemapTurret3()
    {
        settingManager.Turret3.Disable();

        var rebindOperation = settingManager.Turret3.PerformInteractiveRebinding()
                    // To avoid accidental input from mouse motion
                    .WithControlsExcluding("Mouse")
                    .OnMatchWaitForAnother(0.1f)
                    .Start();
        settingManager.Turret3.Enable();
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
