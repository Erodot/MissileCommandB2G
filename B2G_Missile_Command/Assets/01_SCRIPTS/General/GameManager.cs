using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    //Coline Marchal

    //public static GameManager instance;
    public GameOverAndCie ui;
    GameObject[] playerProperty;
    //..Coline Marchal

    public ControlSettings controlSettings;

    public bool controlKeyboard;
    public bool isShieldActivated;

    public GameObject Field;
    public float lerpTime;

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
    public bool turretCanShoot; //ShootingZone used this bool for knowing if the turret can shoot or not.

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

    //public List<ShootingZoneTest> ShootingZoneList = new List<ShootingZoneTest>(); //rename turretList to recupere a game object list
    public List<GameObject> TurretList = new List<GameObject>();
    public List<GameObject> TurretList2 = new List<GameObject>();
    public List<GameObject> CitiesList = new List<GameObject>();
    public List<GameObject> BuildingList = new List<GameObject>();

    bool gameOver;
    bool victory;
    bool terrainOK;

    public GameObject LastActivated;

    [Header("Silver Bullet Manager")]
    [Tooltip("Number of enney to kill to activate the silver bullet.")]
    public int silverBulletMax;
    public int silverBulletCount;
    public GameObject silverBullet;
    public GameObject silverBulletText;

    public bool startGame;

    bool switchRightPressed;
    bool switchLeftPressed;

    public Dictionary<string, int> enemyKill = new Dictionary<string, int>();

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

    void Awake()
    {
        Time.timeScale = 1;

        enemyKill.Add("simple", 0);
        enemyKill.Add("armored", 0);
        enemyKill.Add("virgule", 0);
        enemyKill.Add("hive", 0);
        enemyKill.Add("bomber", 0);
    }

    public void Init()
    {
        playerProperty = GameObject.FindGameObjectsWithTag("Player");
        List<GameObject> allBuildings = new List<GameObject>();

        foreach (GameObject go in playerProperty)
        {
            if (go.name.Contains("Turret"))
            {
                TurretList.Add(go);
                TurretList2.Add(go);
                //ShootingZoneList.Add(go.transform.Find("Zone").gameObject.GetComponent<ShootingZoneTest>());
            }
            else if (go.name.Contains("City"))
            {
                CitiesList.Add(go);
            }
            allBuildings.Add(go);
        }
        #region buildingList
        //sort by position
        BuildingList.Add(allBuildings[1]);
        BuildingList.Add(allBuildings[3]);
        BuildingList.Add(allBuildings[4]);
        BuildingList.Add(allBuildings[0]);
        BuildingList.Add(allBuildings[5]);
        BuildingList.Add(allBuildings[6]);
        BuildingList.Add(allBuildings[2]);
        BuildingList.Add(allBuildings[7]);
        BuildingList.Add(allBuildings[8]);
        #endregion

        terrainOK = true;

        LastActivated = TurretList[1];
        LastActivated.GetComponent<Shoot>().isActivated = true;

        startGame = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(silverBulletCount == silverBulletMax)
        {
            Instantiate(silverBullet, transform);
            silverBulletText.SetActive(true);
            silverBulletCount++;
        }

        if (terrainOK)
        {
            CheckGame();
        }

        /*if (Mathf.RoundToInt(controlSettings.Turret1.ReadValue<float>()) == 1 && TurretList2[0] != null)         
        {             
            if (LastActivated != null)             
            {                 
                LastActivated.GetComponent<Shoot>().isActivated = false;             
            }             
            TurretList2[0].GetComponent<Shoot>().isActivated = true;             
            LastActivated = TurretList2[0];         
        }         
        if (Mathf.RoundToInt(controlSettings.Turret2.ReadValue<float>()) == 1 && TurretList2[1] != null)         
        {             
            if (LastActivated != null)            
            {                 
                LastActivated.GetComponent<Shoot>().isActivated = false;             
            }             
            TurretList2[1].GetComponent<Shoot>().isActivated = true;             
            LastActivated = TurretList2[1];         
        }
        if (Mathf.RoundToInt(controlSettings.Turret3.ReadValue<float>()) == 1 && TurretList2[2] != null)
        {
            if (LastActivated != null)
            {
                LastActivated.GetComponent<Shoot>().isActivated = false;
            }
            TurretList2[2].GetComponent<Shoot>().isActivated = true;
            LastActivated = TurretList2[2];
        }*/

        if (Mathf.RoundToInt(controlSettings.SwitchRight.ReadValue<float>()) == 1 && !switchRightPressed)
        {
            int index = (LastActivated.GetComponent<Shoot>().indexTurret + 1) % TurretList2.Count;
            if(TurretList.Count > 0)
            {
                while (TurretList2[index].GetComponent<Shoot>().isDestroy)
                {
                    index = (index + 1) % TurretList2.Count;
                    Debug.Log(TurretList2[index].GetComponent<Shoot>().isDestroy);
                }
            }

            switchRightPressed = true;
            if (LastActivated != null)
            {
                LastActivated.GetComponent<Shoot>().isActivated = false;
            }
            LastActivated = TurretList2[index];
            LastActivated.GetComponent<Shoot>().isActivated = true;
            LastActivated.transform.Find("TurretSelected").GetComponent<ParticleSystem>().Play();
        }
        if(Mathf.RoundToInt(controlSettings.SwitchRight.ReadValue<float>()) == 0 && switchRightPressed)
        {
            switchRightPressed = false;
        }

        if (Mathf.RoundToInt(controlSettings.SwitchLeft.ReadValue<float>()) == 1 && !switchLeftPressed)
        {
            int index = 0;
            if (LastActivated.GetComponent<Shoot>().indexTurret > 0)
            {
                index = LastActivated.GetComponent<Shoot>().indexTurret - 1;
            }
            else
            {
                index = TurretList2.Count - 1;
            }

            if(TurretList.Count > 0)
            {
                while (TurretList2[index].GetComponent<Shoot>().isDestroy)
                {
                    if (TurretList2[index].GetComponent<Shoot>().indexTurret > 0)
                    {
                        index = index- 1;
                    }
                    else
                    {
                        index = TurretList2.Count - 1;
                    }
                }
            }

            switchLeftPressed = true;
            if (LastActivated != null)
            {
                LastActivated.GetComponent<Shoot>().isActivated = false;
            }
            LastActivated = TurretList2[index];
            LastActivated.GetComponent<Shoot>().isActivated = true;
            LastActivated.transform.Find("TurretSelected").GetComponent<ParticleSystem>().Play();
        }
        if (Mathf.RoundToInt(controlSettings.SwitchLeft.ReadValue<float>()) == 0 && switchLeftPressed)
        {
            switchLeftPressed = false;
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

    /*public void CheckNeighbour(GameObject go)
    {
        //Debug.Log("neigbhour");

        int index = BuildingList.IndexOf(go);
        int nIndex1; //neigbhour 1
        int nIndex2; //neigbhour 2

        if(index == 0)
        {
            nIndex1 = 1;
            nIndex2 = BuildingList.Count-1;
        }
        else if(index == BuildingList.Count-1)
        {
            nIndex1 = index - 1;
            nIndex2 = 0;
        }
        else
        {
            nIndex1 = index - 1;
            nIndex2 = index + 1;
        }
        //Debug.Log(nIndex1 + " | "+ index + " | "+ nIndex2);

        BuildingList[index].GetComponent<BuildingLifeDamage>().DestroyThis();

        if(BuildingList[nIndex1] != null)
            BuildingList[nIndex1].GetComponent<BuildingLifeDamage>().Damaged();

        if (BuildingList[nIndex2] != null)
            BuildingList[nIndex2].GetComponent<BuildingLifeDamage>().Damaged();
    }
    public void CheckNeighbour(GameObject go, string rightOrLeft) //hit the ground
    {
        //Debug.Log("neigbhour");

        int index = 0;
        int nIndex1; //neigbhour 1
        int nIndex2; //neigbhour 2

        if(rightOrLeft == "left")
        {
            nIndex1 = BuildingList.IndexOf(go);
            nIndex2 = nIndex1 + 2;
            index = nIndex1 + 1;
        }
        else if(rightOrLeft == "right")
        {
            nIndex1 = BuildingList.IndexOf(go);
            //nIndex2 = BuildingList.IndexOf(go);
            nIndex2 = nIndex1 - 2;
            //nIndex1 = nIndex2 - 2;
            index = nIndex1 + 1;
        }

        if (index == 0)
        {
            nIndex1 = 1;
            nIndex2 = BuildingList.Count - 1;
        }
        else if (index == BuildingList.Count - 1)
        {
            nIndex1 = index - 1;
            nIndex2 = 0;
        }
        else
        {
            nIndex1 = index - 1;
            nIndex2 = index + 1;
        }

        float distanceToBase;
        if (BuildingList[nIndex1] != null)
        {
            //if the closest object was on the right and the left one is no to far awar
            BuildingList[nIndex1].GetComponent<BuildingLifeDamage>().Damaged();
        }
        if (BuildingList[nIndex2] != null)
        {
            distanceToBase = Vector3.Distance(go.transform.position, BuildingList[nIndex2].transform.position); //distance between the clostest turret/city and the closest in the other direction
            Debug.Log(distanceToBase + "  " + BuildingList[nIndex2].name);
            if (distanceToBase != 0 && distanceToBase < 3f)
            {
                //if the closest object was on the left and the right one is no to far awar

                BuildingList[nIndex2].GetComponent<BuildingLifeDamage>().Damaged();

            }
        }
        Debug.Log("debug");
    }*/

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
                if (go != null && go.name.Contains("Turret"))
                {
                    //go.transform.Find("Zone").gameObject.GetComponent<ShootingZoneTest>();
                    go.GetComponent<Shoot>().enabled = false;
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

    //Julien MACHADO GCC2

    public void ReviveTurret()
    {
        foreach(GameObject turret in TurretList2)
        {
            if (turret.GetComponent<Shoot>().isDestroy && turret.GetComponent<BuildingLifeDamage>().destroyed)
            {
                turret.GetComponent<Shoot>().enabled = true;
                turret.GetComponent<Shoot>().isDestroy = false;
                turret.GetComponent<BuildingLifeDamage>().destroyed = false;
                turret.GetComponent<BuildingLifeDamage>().isDestroy = false;
                turret.GetComponent<BuildingLifeDamage>().lifes = 2;

                turret.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                turret.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
                turret.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);

                TurretList.Add(turret);

                break;
            }
        }
    }

    //..Julien MACHADO GCC2
}
