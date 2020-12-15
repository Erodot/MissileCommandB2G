using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartGame : MonoBehaviour
{
    public GameObject generalPanel;
  

    // Update is called once per frame
    void Update()
    {
        if (Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            gameObject.SetActive(false);
            generalPanel.SetActive(true);
        }
    }
}
