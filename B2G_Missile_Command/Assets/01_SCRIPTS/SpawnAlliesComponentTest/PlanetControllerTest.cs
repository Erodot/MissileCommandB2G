﻿using System.Collections;
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
        fieldTransform.rotation = Quaternion.Euler(0, 0, horizontalMove); //Rotation is refreshed every single frame | Rotation at Z axis.

        if (isRotationAccurate == true)
        {
            if (gameManager.controlKeyboard)
            {
                if (Keyboard.current.rightArrowKey.isPressed)
                {
                    horizontalMove -= 1 * rotationSpeed * Time.deltaTime; //The planet is rotating accurately by horizontal positive and negative button.
                }
                else if (Keyboard.current.leftArrowKey.isPressed)
                {
                    horizontalMove -= -1 * rotationSpeed * Time.deltaTime; //The planet is rotating accurately by horizontal positive and negative button.
                }
            }
            horizontalMove -= controlSettings.Rotate.ReadValue<float>() * rotationSpeed * Time.deltaTime; //The planet is rotating smoothly by horizontal positive and negative button.

        }
        else
        {
            if (gameManager.controlKeyboard)
            {
                if (Keyboard.current.rightArrowKey.isPressed)
                {
                    horizontalMove -= 1 * rotationSpeed * Time.deltaTime; //The planet is rotating smoothly by horizontal positive and negative button.
                }
                else if (Keyboard.current.leftArrowKey.isPressed)
                {
                    horizontalMove -= -1 * rotationSpeed * Time.deltaTime; //The planet is rotating smoothly by horizontal positive and negative button.
                }
            }
            horizontalMove -= controlSettings.Rotate.ReadValue<float>() * rotationSpeed * Time.deltaTime; //The planet is rotating smoothly by horizontal positive and negative button.
        }
    }
    //..Corentin SABIAUX GCC2
}
