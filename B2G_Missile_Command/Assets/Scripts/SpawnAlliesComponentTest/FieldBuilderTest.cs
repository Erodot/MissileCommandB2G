using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldBuilderTest : MonoBehaviour //Script by Corentin SABIAUX GCC2, don't hesitate to ask some questions.
{
    //This script have to be set into a blank gameObject.

    public int turretNumbers; //We need 3 to fit the original game.
    public GameObject Turret;

    public List<Vector3> positionTurretList = new List<Vector3>(); //Size = turretNumbers
    public List<Vector3> rotationTurretList = new List<Vector3>(); //Size = turretNumbers
    private List<GameObject> TurretList = new List<GameObject>(); //Internal Use.

    public int cityNumbers; //We need 6 to fit the original game.
    public float builderWaiting;
    public GameObject City;

    public List<Vector3> positionCityList = new List<Vector3>(); //Size = cityNumbers
    public List<Vector3> rotationCityList = new List<Vector3>(); //Size = cityNumbers
    private List<GameObject> CityList = new List<GameObject>(); //Internal Use.

    void Start()
    {
        StartCoroutine(CreateField()); //Launch FieldBuilder during the first frame of the scene.
    }

    IEnumerator CreateField()
    {
        //Let's create some turrets.

        GameObject TurretCategorie = new GameObject("Turrets"); //We create a category into the Unity Scene for better management.
        TurretCategorie.transform.parent = transform; //We set this category as a child of the field.
        for (int i = 0; i < turretNumbers; i++) //How many turrets do you want ?
        {
            GameObject TurretCreated = Instantiate(Turret, positionTurretList[i], Quaternion.Euler(rotationTurretList[i])); //A new turret is born.
            TurretCreated.transform.parent = TurretCategorie.transform; //Turret became children of spawner.
            TurretList.Add(TurretCreated);
            TurretCreated.name = "IGTurret " + (i + 1); //Set turret name.
            yield return new WaitForSeconds(builderWaiting); //How many times do you want to wait before construct the next city ?
        }

        //Let's create some cities.
         
        GameObject CityCategorie = new GameObject("Cities"); //We create a category into the Unity Scene for better management.
        CityCategorie.transform.parent = transform; //We set this category as a child of the field.
        for (int i = 0; i < cityNumbers; i++) //How many cities do you want ?
        {
            GameObject CityCreated = Instantiate(City, positionCityList[i], Quaternion.Euler(rotationCityList[i])); //A new city is born.
            CityCreated.transform.parent = CityCategorie.transform; //City became children of spawner.
            CityList.Add(CityCreated);
            CityCreated.name = "IGCity " + (i + 1); //Set City name | Welcome to City 17.
            yield return new WaitForSeconds(builderWaiting); //How many times do you want to wait before construct the next city ?
        }
    }
}
