using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlanetControllerTest : MonoBehaviour 
{
    public GameManager gameManager;
    public ControlSettings controlSettings;

    //Script by Corentin SABIAUX GCC2, don't hesitate to ask some questions.

    [Header("Get the Planet")]
    [Header("Planet Controller Manager")]
    [Tooltip("We need the controller from the gameObject field.")]
    public CharacterController controller;
    [Tooltip("We need the transform from the gameObject field.")]
    public Transform fieldTransform;

    [Header("Speed and Rotation")]
    [Tooltip("Here, you can choose the speed of rotation.")]
    public float rotationSpeed = 1f;
    private float horizontalMove = 0f; //Internal-use, we need it for stocking the movement.
    [Tooltip("Check-it if you want to have a more accurate rotation of the planet.")]
    public bool isRotationAccurate;


    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {

        if (isRotationAccurate == true)
        {
            if (gameManager.controlKeyboard)
            {
                fieldTransform.rotation = Quaternion.Euler(0, 0, horizontalMove); //Rotation is refreshed every single frame | Rotation at Z axis.

                if (Keyboard.current.rightArrowKey.isPressed)
                {
                    horizontalMove -= 1 * rotationSpeed * Time.deltaTime; //The planet is rotating accurately by horizontal positive and negative button.
                }
                else if (Keyboard.current.leftArrowKey.isPressed)
                {
                    horizontalMove -= -1 * rotationSpeed * Time.deltaTime; //The planet is rotating accurately by horizontal positive and negative button.
                }
            }
            if (controlSettings.RotateX.ReadValue<float>() != 0 || controlSettings.RotateY.ReadValue<float>() != 0)
            {
                float a = controlSettings.RotateX.ReadValue<float>() + controlSettings.RotateY.ReadValue<float>();
                float b = controlSettings.RotateX.ReadValue<float>() - controlSettings.RotateY.ReadValue<float>();
                if (a >= 1 || b <= -1 || b >= 1 || a <= -1)
                {
                    gameObject.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(-controlSettings.RotateX.ReadValue<float>(), -controlSettings.RotateY.ReadValue<float>()) * 180 / Mathf.PI);
                }
            }
            //horizontalMove -= controlSettings.RotateX.ReadValue<float>() * rotationSpeed * Time.deltaTime; //The planet is rotating smoothly by horizontal positive and negative button.

        }
        else
        {
            if (gameManager.controlKeyboard)
            {
                fieldTransform.rotation = Quaternion.Euler(0, 0, horizontalMove); //Rotation is refreshed every single frame | Rotation at Z axis.

                if (Keyboard.current.rightArrowKey.isPressed)
                {
                    horizontalMove -= 1 * rotationSpeed * Time.deltaTime; //The planet is rotating smoothly by horizontal positive and negative button.
                }
                else if (Keyboard.current.leftArrowKey.isPressed)
                {
                    horizontalMove -= -1 * rotationSpeed * Time.deltaTime; //The planet is rotating smoothly by horizontal positive and negative button.
                }
            }
            if (controlSettings.RotateX.ReadValue<float>() != 0 || controlSettings.RotateY.ReadValue<float>() != 0)
            {
                float a = controlSettings.RotateX.ReadValue<float>() + controlSettings.RotateY.ReadValue<float>();
                float b = controlSettings.RotateX.ReadValue<float>() - controlSettings.RotateY.ReadValue<float>();
                if (a >= 1 || b <= -1 || b >= 1 || a <= -1)
                {
                        gameObject.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(-controlSettings.RotateX.ReadValue<float>(), -controlSettings.RotateY.ReadValue<float>()) * 180 / Mathf.PI);  
                }
            }
            //horizontalMove -= controlSettings.RotateX.ReadValue<float>() * rotationSpeed * Time.deltaTime; //The planet is rotating smoothly by horizontal positive and negative button.
        }
    }
    //..Corentin SABIAUX GCC2
}
