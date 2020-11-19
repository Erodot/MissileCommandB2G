using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnTest2 : MonoBehaviour
{
    //MACHADO Julien

    Vector3 screenPos;

    [SerializeField]
    GameObject simpleEnemy;
    public GameObject armoredEnemy;

    public int waveNumber;



    int difficultyAugmentation;

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
        if (enemyToSpawn > 0) //while there's still enemy to spawn
        {
            if (actualTime == timeToSpawn) //if the actual time is the time to spawn enemy
            {
                InstantiateEnemy(); //spawn an enemy
            }

            if (actualTime > 0) //if the time has'nt reached 0
            {
                actualTime -= Time.deltaTime; //make it decrease
            }
            else
            {
                actualTime = timeToSpawn; //reset the clock
            }
        }
        else
        {

            if (actualTime2 > 0) //if the time has'nt reached 0
            {
                actualTime2 -= Time.deltaTime; //make it decrease
            }
            else
            {
                int citiesNumber = 0;
                timeBetweenWaveStart -= Random.Range(timeBetweenWaveReductionMin, timeBetweenWaveReductionMax);
                foreach (GameObject cities in gameManager.CitiesList)
                {
                    if (cities != null && cities.GetComponent<MeshRenderer>())
                    {
                        citiesNumber += 1;
                    }
                }
                if (citiesNumber <= 6 && citiesNumber > 4)
                {
                    difficultyAugmentation = 3;
                    //Debug.Log("3");
                }
                else if (citiesNumber <= 4 && citiesNumber > 2)
                {
                    difficultyAugmentation = 2;
                    //Debug.Log("2");
                }
                else if (citiesNumber <= 2)
                {
                    difficultyAugmentation = 1;
                    //Debug.Log("1");
                }
                timeToSpawn -= ((waveNumber + 1) * difficultyAugmentation) / (10 * timeDiviser);
                //Debug.Log(timeToSpawn);
                actualTime = timeToSpawn;


                enemyToSpawn = enemyToSpawnBank + Random.Range(enemyGainMin, enemyGainMax) + difficultyAugmentation;
                enemyToSpawnBank = enemyToSpawn;
                actualTime2 = timeBetweenWaveStart;
                
                LevelsManager lvlManager = LevelsManager.instance;
                lvlManager.currentLevel += 1;
                waveNumber += 1;
                wave.text = "\r\n" + waveNumber;
            }
            //StartCoroutine(Wave());
        }
    }

    void InstantiateEnemy()
    {
        int whereSpawn;
        whereSpawn = Random.Range(0, 4); //choose between 4 place to spawn, top, bottom, left, right
        if (whereSpawn == 0) //top
        {
            //Nicolas Pupulin
            int ennemyType = Random.Range(0, 2); //Choose between 2 type of ennemy
            if (ennemyType == 0) //Simple ennemy
            {
                screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Screen.height, 10)); //pick a random place on the top of the screen

                GameObject go = Instantiate(simpleEnemy, screenPos, Quaternion.identity); //spawn an enemy at this random place
            }
            else if (ennemyType == 1) //Armored ennemy 
            {
                screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Screen.height, 10)); //pick a random place on the top of the screen

                GameObject ga = Instantiate(armoredEnemy, screenPos, Quaternion.identity); //spawn an enemy at this random place
            }
            //..Nicolas Pupulin
        }
        else if (whereSpawn == 1) //left
        {
            //Nicolas Pupulin
            int ennemyType = Random.Range(0, 2); //Choose between 2 type of ennemy

            if (ennemyType == 0) //Simple ennemy
            {
                screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Random.Range(0, Screen.height), 10)); //pick a random place on the right of the screen

                GameObject go = Instantiate(simpleEnemy, screenPos, Quaternion.identity); //spawn an enemy at this random place
            }
            else if (ennemyType == 1) //Armored ennemy 
            {
                screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Random.Range(0, Screen.height), 10)); //pick a random place on the right of the screen

                GameObject ga = Instantiate(armoredEnemy, screenPos, Quaternion.identity); //spawn an enemy at this random place
            }
            //..Nicolas Pupulin
        }
        else if (whereSpawn == 2) //bottom
        {
            //Nicolas Pupulin
            int ennemyType = Random.Range(0, 2); //Choose between 2 type of ennemy

            if (ennemyType == 0) //Simple ennemy
            {
                screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), -5.5f, 10)); //pick a random place on the bottom of the screen

                GameObject go = Instantiate(simpleEnemy, screenPos, Quaternion.identity); //spawn an enemy at this random place
            }
            else if (ennemyType == 1) //Armored ennemy 
            {
                screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), -5.5f, 10)); //pick a random place on the bottom of the screen

                GameObject ga = Instantiate(armoredEnemy, screenPos, Quaternion.identity); //spawn an enemy at this random place
            }
            //..Nicolas Pupulin
        }
        else if (whereSpawn == 3) //right
        {
            //Nicolas Pupulin
            int ennemyType = Random.Range(0, 2); //Choose between 2 type of ennemy

            if (ennemyType == 0) //Simple ennemy
            {
                screenPos = Camera.main.ScreenToWorldPoint(new Vector3(-9.5f, Random.Range(0, Screen.height), 10)); //pick a random place on the left of the screen

                GameObject go = Instantiate(simpleEnemy, screenPos, Quaternion.identity); //spawn an enemy at this random place
            }
            else if (ennemyType == 1) //Armored ennemy 
            {
                screenPos = Camera.main.ScreenToWorldPoint(new Vector3(-9.5f, Random.Range(0, Screen.height), 10)); //pick a random place on the left of the screen

                GameObject ga = Instantiate(armoredEnemy, screenPos, Quaternion.identity); //spawn an enemy at this random place
            }
            //..Nicolas Pupulin
        }
        enemyToSpawn -= 1; //decrease the enemy spawn counter
    }

    //..MACHADO Julien
}
