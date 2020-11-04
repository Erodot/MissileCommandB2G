using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetControllerTest : MonoBehaviour //Script by Corentin SABIAUX GCC2, don't hesitate to ask some questions.
{
    public CharacterController controller; //We need the controller from the gameObject field.
    public Transform fieldTransform; //We need the transform from the gameObject field.

    public float rotationSpeed = 1f; //Here, you can choose the speed of rotation.
    private float horizontalMove = 0f;

    public bool isRotationAccurate; //Internal use, check-it if you want to have a more accurate rotation of the planet.

    // Update is called once per frame
    void Update()
    {
        fieldTransform.rotation = Quaternion.Euler(0, 0, horizontalMove); //Rotation is refreshed every single frame | Rotation at Z axis.

        if (isRotationAccurate == true)
        {
            horizontalMove += Input.GetAxisRaw("Horizontal") * rotationSpeed * Time.deltaTime; //The planet is rotating accurately by horizontal positive and negative button.
        } else
        {
            horizontalMove += Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime; //The planet is rotating smoothly by horizontal positive and negative button.
        }
    }

    //..Corentin SABIAUX GCC2
}
