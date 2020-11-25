﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnTest2 : MonoBehaviour
{
    //MACHADO Julien

    Vector3 screenPos;

    public GameObject[] enemys;

    public int waveNumber;



    int difficultyAugmentation;
    public int difficultySpawn;
    public int diffModifier = 1;

    [Header("Enemy augmentation")]
    [Range(0, 10), Tooltip("Maximum addition of enemy for a wave")]
    public int enemyGainMax;
    [Range(0, 10), Tooltip("Minimum addition of enemy for a wave")]
    public int enemyGainMin;

    [Header("Enemy basic spawn")]
    int enemyToSpawnBank;
    public int enemyToSpawn; //number of enemy to spawn
    [Range(0, 50)]
    public int enemyMin; //number min of enemy to spawn
    [Range(0, 50)]
    public int enemyMax; //number max of enemy to spawn

    [Header("Time between waves Manager")]
    [Range(0.0f, 10.0f), Tooltip("The time between wave at start")]
    public float timeBetweenWaveStart;
    [Range(0.0f, 5.0f), Tooltip("Maximum reduction that can have the time between wave")]
    public float timeBetweenWaveReductionMax;
    [Range(0.0f, 5.0f), Tooltip("Minimum reduction that can have the time between wave")]
    public float timeBetweenWaveReductionMin;
    float actualTime2; //actual time of the clock

    [Header("Time between enemy spawn Manger")]
    [Range(0.0f, 5.0f)]
    public float timeToSpawn; //time between the enemy spawn
    [Range(1.0f, 5.0f), Tooltip("Number used to divide the time between the enemy spawn")]
    public float timeDiviser;
    float actualTime; //actual time of the clock

    public GameManager gameManager;

    public Text wave;




    // Start is called before the first frame update
    void Start()
    {
        enemyToSpawn = Random.Range(enemyMin, enemyMax); //choose the number of enemy to spawn between enemyMin and enemyMax
        enemyToSpawnBank = enemyToSpawn;
        actualTime = timeToSpawn; //set the actual time to to the time chosen for the clock
        actualTime2 = timeBetweenWaveStart;
        wave.text = "\r\n" + waveNumber;
    }

    // Update is called once per frame
    void Update()
    {
        //clock for the enemy spawning
        if (enemyToSpawn > 0) //while there's still enemy to spawn in the wave
        {
            if (actualTime == timeToSpawn) //if the actual time is the time to spawn enemy
            {
                InstantiateEnemy(); //spawn an enemy
            }

            if (actualTime > 0) //if the time between enemy spawn has'nt reached 0
            {
                actualTime -= Time.deltaTime; //make it decrease
            }
            else
            {
                actualTime = timeToSpawn; //reset the clock
            }
        }
        else //if the wave has ended
        {

            if (actualTime2 > 0) //if the time between wave has'nt reached 0
            {
                actualTime2 -= Time.deltaTime; //make it decrease
            }
            else 
            {
                int citiesNumber = 0; //city count number
                timeBetweenWaveStart -= Random.Range(timeBetweenWaveReductionMin, timeBetweenWaveReductionMax); //decrease the time between wave
                foreach (GameObject cities in gameManager.CitiesList) //check for all the building
                {
                    if (cities != null && cities.GetComponent<MeshRenderer>()) //if he is still alive
                    {
                        citiesNumber += 1; //add it to the city count
                    }
                }
                if (citiesNumber <= 6 && citiesNumber > 4) //if there is between 4 and 6 cities left
                {
                    difficultyAugmentation = 3; //set difficulty increment to 3
                }
                else if (citiesNumber <= 4 && citiesNumber > 2)  //if there is between 2 and 4 cities left
                {
                    difficultyAugmentation = 2; //set difficulty increment to 2
                }
                else if (citiesNumber <= 2)  //if there is less than 2 cities left
                {
                    difficultyAugmentation = 1; //set difficulty increment to 1
                }
                timeToSpawn -= ((waveNumber + 1) * difficultyAugmentation) / (10 * timeDiviser); //lower the time between enemy spawn 
                actualTime = timeToSpawn; //set the actual time between enemy spawn


                enemyToSpawn = enemyToSpawnBank + Random.Range(enemyGainMin, enemyGainMax) + difficultyAugmentation; //choose the number of ennemy to spawn for the next wave
                enemyToSpawnBank = enemyToSpawn; //reset the max enemy spawn
                actualTime2 = timeBetweenWaveStart; //reset the actual time between wave
                difficultySpawn += difficultyAugmentation; //add to the difficulty for the spawn
                if (difficultySpawn >= 9) //if the difficulty for the spawn is higher or equal to 9
                {
                    if (diffModifier < enemys.Length) //if the difficulty of enemy is lower than the number of enemy
                    {
                        diffModifier++; 
                    }
                    difficultySpawn = 0; //reset the difficulty for the spawn
                }

                LevelsManager lvlManager = LevelsManager.instance; //get the level manager
                lvlManager.currentLevel += 1; 
                waveNumber += 1; // add one to the wave number
                wave.text = "\r\n" + waveNumber; //set the text on screen
            }
            //StartCoroutine(Wave());
        }
    }

    void InstantiateEnemy()
    {
        int whereSpawn;
        whereSpawn = Random.Range(0, 4); //choose between 4 place to spawn, top, bottom, left, right
        //int enemyType = Random.Range(0, enemys.Length); //Choose between all type of enemys
        if (whereSpawn == 0) //top
        {
            screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Screen.height, 10)); //pick a random place on the top of the screen

            GameObject go = Instantiate(enemys[randomEnemy()], screenPos, Quaternion.identity); //spawn a random enemy at this random place
        }
        else if (whereSpawn == 1) //left
        {
            screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Random.Range(0, Screen.height), 10)); //pick a random place on the right of the screen

            GameObject go = Instantiate(enemys[randomEnemy()], screenPos, Quaternion.identity); //spawn a random enemy at this random place
        }
        else if (whereSpawn == 2) //bottom
        {
            screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), -5.5f, 10)); //pick a random place on the bottom of the screen

            GameObject go = Instantiate(enemys[randomEnemy()], screenPos, Quaternion.identity); //spawn a random enemy at this random place
        }
        else if (whereSpawn == 3) //right
        {
            screenPos = Camera.main.ScreenToWorldPoint(new Vector3(-9.5f, Random.Range(0, Screen.height), 10)); //pick a random place on the left of the screen

            GameObject go = Instantiate(enemys[randomEnemy()], screenPos, Quaternion.identity); //spawn a random enemy at this random place
        }
        enemyToSpawn -= 1; //decrease the enemy spawn counter
    }

    int randomEnemy()
    {
        int rngSpawn = Random.Range(1, 101); //pick a number between 1 and 100
        int diffCoeff = 100 / diffModifier; //lower it to be between 1 and the diff modifier

        rngSpawn = Mathf.RoundToInt(rngSpawn / diffCoeff); //round it to get an int
        return rngSpawn; //return this number
    }

    //..MACHADO Julien
}
