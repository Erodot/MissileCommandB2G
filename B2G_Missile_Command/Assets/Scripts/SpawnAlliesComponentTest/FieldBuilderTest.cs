using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldBuilderTest : MonoBehaviour 
{
    //Script by Corentin SABIAUX GCC2, don't hesitate to ask some questions.
    //This script have to be set into a blank gameObject.

    [Header("Turret Manager")]
    [Tooltip("We need 3 turrets to fit the original game.")]
    public int turretNumbers;
    [Tooltip("Prefab of the turret.")]
    public GameObject Turret;
    [Tooltip("You need to set the position of every turret on this list | Size = turretNumbers.")]
    public List<Vector3> positionTurretList = new List<Vector3>();
    [Tooltip("You need to set the angle of every turret on this list | Size = turretNumbers.")]
    public List<Vector3> rotationTurretList = new List<Vector3>();
    private List<GameObject> TurretList = new List<GameObject>(); //Internal Use, if you want to getComponent of a specific turret.

    [Header("City Manager")]
    [Tooltip("We need 6 cities to fit the original game.")]
    public int cityNumbers;
    [Tooltip("Prefab of the city.")]
    public GameObject City;
    [Tooltip("You need to set the position of every city on this list | Size = cityNumbers.")]
    public List<Vector3> positionCityList = new List<Vector3>();
    [Tooltip("You need to set the position of every city on this list | Size = cityNumbers.")]
    public List<Vector3> rotationCityList = new List<Vector3>();
    private List<GameObject> CityList = new List<GameObject>(); //Internal Use, if you want to getComponent of a specific city.

    [Header("Time Builder Manager")]
    [Tooltip("Set the waiting time you want for every gameObject instantiate.")]
    public float builderWaiting;

    [HideInInspector]
    public bool builderIsOver = false; //Internal Use, tell to others scripts that FieldBuilder had finished is work.

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
        //..Corentin SABIAUX GCC2

        //MACHADO Julien
        //Let's activate the spawner.
        GameObject spawner = GameObject.Find("Spawner");
        spawner.GetComponent<EnemySpawnTest2>().enabled = true;
        //..MACHADO Julien

        //Corentin SABIAUX GCC2
        //Let's activate the planet rotation by Horizontal input axis.
        GetComponent<PlanetControllerTest>().enabled = true;

        //Let's activate the fire capability of the turrets.
        for (int i = 0; i < turretNumbers; i++)
        {
            TurretList[i].GetComponent<TurretAllie>().enabled = true;
        }
        //..Corentin SABIAUX GCC2

        //Coline Marchal
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameManager != null)
        {
            gameManager.Init();
        }
        //..Coline Marchal

        //Corentin SABIAUX GCC2
        builderIsOver = true;
        //..Corentin SABIAUX GCC2
    }
}
