using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlSettings : MonoBehaviour
{
    public InputAction RotateX;
    public InputAction RotateY;
    public InputAction Shoot;
    public InputAction Turret1;
    public InputAction Turret2;
    public InputAction Turret3;
    public InputAction Pause;

    void OnEnable()
    {
        RotateX.Enable();
        RotateY.Enable();
        Shoot.Enable();
        Turret1.Enable();
        Turret2.Enable();
        Turret3.Enable();
        Pause.Enable();
    }

    void OnDisable()
    {
        RotateX.Disable();
        RotateY.Disable();
        Shoot.Disable();
        Turret1.Disable();
        Turret2.Disable();
        Turret3.Disable();
        Pause.Disable();
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
