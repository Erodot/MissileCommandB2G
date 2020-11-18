﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Coline Marchal

    //public static GameManager instance;
    public GameOverAndCie ui;
    GameObject[] playerProperty;
    //..Coline Marchal

    //Corentin SABIAUX GCC2
    [Header("During Game Over and Victory scene")]
    [Tooltip("Check-it if you want to stop ennemySpawn during the gameover scene.")]
    public bool stopEnemySpawner;
    [Tooltip("Check-it if you want to stop planetController during the gameover scene.")]
    public bool stopPlanetController;
    [Tooltip("Check-it if you want to stop fire capability of the turrets during the gameover scene.")]
    public bool stopFireCapability;

    [Header("Shooting Manager")]
    [Tooltip("Set the amount of time you want between shoots.")]
    public float shootingCoolDown;
    [HideInInspector]
    public bool turretCanShoot = true; //ShootingZone used this bool for knowing if the turret can shoot or not.

    //The idea here is to used 3 lists nested for having an adjustable shooting zone for every turrets.
    [Tooltip("Adjustable shooting zone for each turrets")]
    public ListOfTurrets listOfTurrets = new ListOfTurrets();

    [System.Serializable]
    public class ListOfTurrets
    {
        [Tooltip("Size = number of turrets. '1' will be the first initialized into the scene.")]
        public List<Point> listTurretZone;
    }

    [System.Serializable]
    public class Point
    {
        [Tooltip("Size = number of points. Content = position X and Y. As we have a triangle, you need to set 3 points.")]
        public List<Vector2> pointsTurretZone;
    }
    //..Corentin SABIAUX GCC2

    //Coline Marchal

    public List<ShootingZoneTest> TurretList = new List<ShootingZoneTest>();
    public List<GameObject> CitiesList = new List<GameObject>();
    bool gameOver;
    bool victory;
    bool terrainOK;

    /*  singleton
    void Awake()
    {
        //singleton
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(this.gameObject);

            //Rest of your Awake code

        }
        else
        {
            Destroy(this);
        }
    }*/

    public void Init()
    {
        playerProperty = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject go in playerProperty)
        {
            if (go.name.Contains("Turret"))
            {
                TurretList.Add(go.transform.Find("Zone").gameObject.GetComponent<ShootingZoneTest>());
            }
            else if (go.name.Contains("City"))
            {
                CitiesList.Add(go);
            }
        }

        terrainOK = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (terrainOK)
        {
            CheckGame();
        }
    }

    void CheckGame()
    {
        if (!CheckCitiesLeft() || !CheckTurretsLeft())//there is no hope
        {
            ui.GameOver();
            gameOver = true;
            //..Coline Marchal

            //Corentin SABIAUX GCC2

            StopInteractiveElements();
        }

        /*if (GameObject.Find("Spawner").GetComponent<EnemySpawnTest2>().enemyToSpawn <= 0 && GameObject.Find("Field").GetComponent<FieldBuilderTest>().builderIsOver == true && !GameObject.Find("Capsule(Clone)") && !gameOver)
        //Check if there's no more ennemy to spawn, if the builder field is over and if there's no ennemies bullets into the scene.
        {
            ui.Victory(); //Call victory screen.
            victory = true;

            StopInteractiveElements();
            //..Corentin SABIAUX GCC2
        }*/
    }

    //Coline Marchal

    bool CheckCitiesLeft()
    {
        if (CitiesList.Count > 0)
            return true;
        else
            return false;
    }
    bool CheckTurretsLeft()
    {
        if (TurretList.Count > 0)
            return true;
        else
            return false;
    }
    //..Coline Marchal

    //Corentin SABIAUX GCC2

    void StopInteractiveElements()
    {
        //If we need to stop all interactive elements ingame during game over scene ...
        //Let's desactivate the enemy spawner if the GD want to stop it.
        if (stopEnemySpawner == true)
        {
            GameObject spawner = GameObject.Find("Spawner");
            spawner.GetComponent<EnemySpawnTest2>().enabled = false;
        }

        //Let's desactivate the PlanetController if the GD want to stop it.
        if (stopPlanetController == true)
        {
            GameObject builder = GameObject.Find("Field");
            builder.GetComponent<PlanetControllerTest>().enabled = false;
        }

        //Let's desactivate the turret FireCapability if the GD want to stop it.
        if (stopFireCapability == true)
        {
            foreach (GameObject go in playerProperty)
            {
                if (go.name.Contains("Turret"))
                {
                    go.transform.Find("Zone").gameObject.GetComponent<ShootingZoneTest>();
                }
            }
        }
    }
    public IEnumerator CoolDown()
    {
        turretCanShoot = false;
        yield return new WaitForSeconds(shootingCoolDown);
        turretCanShoot = true;
    }
    //..Corentin SABIAUX GCC2
}
