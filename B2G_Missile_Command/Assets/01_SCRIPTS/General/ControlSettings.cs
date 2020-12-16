using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlSettings : MonoBehaviour
{
    public InputAction RotateX;
    public InputAction RotateY;
    public InputAction Shoot;
    public InputAction SwitchRight;
    public InputAction SwitchLeft;
    public InputAction Pause;
    public InputAction SilverBullet;

    void OnEnable()
    {
        RotateX.Enable();
        RotateY.Enable();
        Shoot.Enable();
        SwitchRight.Enable();
        SwitchLeft.Enable();
        Pause.Enable();
        SilverBullet.Enable();
    }

    void OnDisable()
    {
        RotateX.Disable();
        RotateY.Disable();
        Shoot.Disable();
        SwitchRight.Disable();
        SwitchLeft.Disable();
        Pause.Disable();
        SilverBullet.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
