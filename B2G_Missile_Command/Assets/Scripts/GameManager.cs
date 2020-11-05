using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public static GameManager instance;
    public GameOverAndCie ui;
    GameObject[] playerProperty;
    public List<TurretAllie> TurretList = new List<TurretAllie>();
    public List<GameObject> CitiesList = new List<GameObject>();
    bool gameOver;
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
        foreach(GameObject go in playerProperty)
        {
            if (go.name.Contains("Turret"))
            {
                TurretList.Add(go.GetComponent<TurretAllie>());
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
        }
    }

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

}
