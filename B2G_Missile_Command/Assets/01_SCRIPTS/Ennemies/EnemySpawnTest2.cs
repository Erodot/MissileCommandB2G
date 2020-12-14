using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnTest2 : MonoBehaviour
{
    //MACHADO Julien

    public Vector3 screenPos;

    public GameObject[] enemys;
    public GameObject bonus;
    public GameObject[] bonusEffect;
    public GameObject[] enemyIcons;
    public GameObject waveAnounce;
    public Text waveAff;
    public Text waveTimer;
    bool showAnouncer = true;

    public GameObject Alert;
    [Tooltip("Life time of the alert on the screen")]
    public float alertDispawnTime;
    [Tooltip("Spawn time of the enemy after the alert")]
    public float afterAlertSpawnTime;


    public int waveNumber;

    public int bonusRngMax;
    public int bonusFixe;
    float bonusTimeInterval;
    float actualTime3;


    int difficultyAugmentation;
    public int difficultySpawn;
    public int diffModifier = 1;

    float _totalSpawnWeight;


    [Header("Enemy augmentation")]
    [Tooltip("Maximum number of enemy during a wave")]
    public int maxEnemy;

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
    float actualTime2; //actual time of the clock

    [Header("Time between enemy spawn Manger")]
    [Range(0.0f, 5.0f)]
    public float timeToSpawn; //time between the enemy spawn
    [Range(1.0f, 5.0f), Tooltip("Number used to divide the time between the enemy spawn")]
    public float timeDiviser;
    [Tooltip("Minimum time possible between wave")]
    public float minimumSpawnTime;
    float actualTime; //actual time of the clock

    public int whereSpawn;

    public EnemyAnnouncerTest announcerScript;

    public GameManager gameManager;

    public Text wave;

    int doorIncrease;

    public bool pacingStart;
    bool start = true;

    // Start is called before the first frame update
    void Start()
    {
        enemyToSpawn = Random.Range(enemyMin, enemyMax); //choose the number of enemy to spawn between enemyMin and enemyMax
        enemyToSpawnBank = enemyToSpawn;
        actualTime = timeToSpawn; //set the actual time to to the time chosen for the clock
        actualTime2 = timeBetweenWaveStart;
        //wave.text = waveNumber.ToString(); // "\r\n" + 
        if (bonusFixe > 0)
        {
            bonusTimeInterval = (enemyToSpawn * timeToSpawn) / (bonusFixe + 1);
        }
        else
        {
            bonusTimeInterval = (enemyToSpawn * timeToSpawn) / Random.Range(1, bonusRngMax + 1);
        }
        actualTime3 = bonusTimeInterval - Time.deltaTime;
        StartCoroutine(WaveAnouncer());
    }

    // Update is called once per frame
    void Update()
    {
        if (!start)
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


                if (actualTime3 == bonusTimeInterval) //if the actual time is the time to spawn bonus
                {
                    InstantiateBonus(); //spawn bonus
                }

                if (actualTime3 > 0) //if the time between bonus spawn has'nt reached 0
                {
                    actualTime3 -= Time.deltaTime; //make it decrease
                }
                else
                {
                    actualTime3 = bonusTimeInterval; //reset the clock
                }

            }
            else if (enemyToSpawn <= 0 && pacingStart)//if the wave has ended
            {
                if (showAnouncer)
                {
                    int citiesNumber = 0; //city count number

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

                    difficultySpawn += difficultyAugmentation; //add to the difficulty for the spawn

                    if (difficultySpawn >= 7) //if the difficulty for the spawn is higher or equal to 9
                    {
                        if (diffModifier < enemys.Length) //if the difficulty of enemy is lower than the number of enemy
                        {
                            diffModifier++;
                            enemyToSpawnBank -= 5;
                        }
                        difficultySpawn = 0; //reset the difficulty for the spawn
                    }
                    waveNumber += 1; // add one to the wave number
                    GameObject.FindGameObjectWithTag("audio").GetComponent<SoundManager>().Play("wave");
                    StartCoroutine(WaveAnouncer());
                    showAnouncer = false;
                }

                waveTimer.text = (Mathf.CeilToInt(actualTime2)).ToString();

                if (actualTime2 > 0) //if the time between wave has'nt reached 0
                {
                    actualTime2 -= Time.deltaTime; //make it decrease
                }
                else
                {
                    if (timeToSpawn > minimumSpawnTime)
                    {
                        timeToSpawn -= ((waveNumber + 1) * difficultyAugmentation) / (10 * timeDiviser); //lower the time between enemy spawn 
                    }

                    actualTime = timeToSpawn; //set the actual time between enemy spawn

                    if (enemyToSpawnBank < maxEnemy)
                    {
                        enemyToSpawn = enemyToSpawnBank + 5; //choose the number of ennemy to spawn for the next wave
                    }

                    enemyToSpawnBank = enemyToSpawn; //reset the max enemy spawn
                    actualTime2 = timeBetweenWaveStart; //reset the actual time between wave

                    if (bonusFixe > 0)
                    {
                        bonusTimeInterval = (enemyToSpawn * timeToSpawn) / (bonusFixe + 1);
                    }
                    else
                    {
                        bonusTimeInterval = (enemyToSpawn * timeToSpawn) / Random.Range(1, bonusRngMax + 1);
                    }
                    actualTime3 = bonusTimeInterval - Time.deltaTime;

                    if (doorIncrease < Screen.width)
                    {
                        doorIncrease += Screen.width / 10;
                    }

                    pacingStart = false;
                    showAnouncer = true;

                    //wave.text = waveNumber.ToString(); // "\r\n" + // set the text on screen
                }

            }
        }
    }

    void InstantiateEnemy()
    {
        whereSpawn = Random.Range(0, 4); //choose between 4 place to spawn, top, bottom, left, right
        //int enemyType = Random.Range(0, enemys.Length); //Choose between all type of enemys
        if (whereSpawn == 0) //top
        {
            if(doorIncrease < Screen.width / 2)
            {
                int door = Random.Range(0, 2);
                if (door == 0)
                {
                    screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, 2 + doorIncrease), Screen.height, 10));
                }
                else
                {
                    screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(Screen.width - 2 - doorIncrease, Screen.width), Screen.height, 10));
                }
            }
            else
            {
                screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Screen.height, 10));
            }


            //GameObject go = Instantiate(enemys[randomEnemy()], screenPos, Quaternion.identity); //spawn a random enemy at this random place
            StartCoroutine(EnemySequencer(screenPos));
        }
        else if (whereSpawn == 1) //left
        {
            if(doorIncrease < Screen.height / 2)
            {
                int door = Random.Range(0, 2);
                if (door == 0)
                {
                    screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Random.Range(0, 2 + doorIncrease), 10));
                }
                else
                {
                    screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Random.Range(Screen.height - 2 - doorIncrease, Screen.height), 10));
                }
            }
            else
            {
                screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Random.Range(0, Screen.height), 10));
            }

            StartCoroutine(EnemySequencer(screenPos));
        }
        else if (whereSpawn == 2) //bottom
        {
            if(doorIncrease < Screen.width / 2)
            {
                int door = Random.Range(0, 2);
                if (door == 0)
                {
                    screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, 2 + doorIncrease), -5.5f, 10));
                }
                else
                {
                    screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(Screen.width - 2 - doorIncrease, Screen.width), -5.5f, 10));
                }
            }
            else
            {
                screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), -5.5f, 10));
            }

            StartCoroutine(EnemySequencer(screenPos));
        }
        else if (whereSpawn == 3) //right
        {
            if(doorIncrease < Screen.height / 2)
            {
                int door = Random.Range(0, 2);
                if (door == 0)
                {
                    screenPos = Camera.main.ScreenToWorldPoint(new Vector3(-9.5f, Random.Range(0, 2 + doorIncrease), 10));
                }
                else
                {
                    screenPos = Camera.main.ScreenToWorldPoint(new Vector3(-9.5f, Random.Range(Screen.height - 2 - doorIncrease, Screen.height), 10));
                }
            }
            else
            {
                screenPos = Camera.main.ScreenToWorldPoint(new Vector3(-9.5f, Random.Range(0, Screen.height), 10));
            }

            StartCoroutine(EnemySequencer(screenPos));
        }
        enemyToSpawn -= 1; //decrease the enemy spawn counter
    }

    int randomEnemy()
    {
        _totalSpawnWeight = 0f;
        for (int i = 0; i < diffModifier; i++)
        {
            _totalSpawnWeight += enemys[i].GetComponent<EnemyMissile>().weight;
        }

        float pick = Random.value * _totalSpawnWeight;
        int chosenIndex = 0;
        float cumulativeWeight = enemys[0].GetComponent<EnemyMissile>().weight;

        // Step through the list until we've accumulated more weight than this.
        // The length check is for safety in case rounding errors accumulate.
        while (pick > cumulativeWeight && chosenIndex < diffModifier - 1)
        {
            chosenIndex++;
            cumulativeWeight += enemys[chosenIndex].GetComponent<EnemyMissile>().weight;
        }

        return chosenIndex;
    }

    void InstantiateBonus()
    {
        int whereSpawn;
        whereSpawn = Random.Range(0, 4); //choose between 4 place to spawn, top, bottom, left, right
        if (whereSpawn == 0) //top
        {
            screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Screen.height, 10)); //pick a random place on the top of the screen

            GameObject go = Instantiate(bonus, screenPos, Quaternion.identity); //spawn a bonus at this random place
            go.GetComponent<bonusdeplac>().direction = 3;
            go.GetComponent<bonusdeplac>().bonusEffect = bonusEffect[Random.Range(0, bonusEffect.Length)];
        }
        else if (whereSpawn == 1) //left
        {
            screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Random.Range(0, Screen.height), 10)); //pick a random place on the right of the screen

            GameObject go = Instantiate(bonus, screenPos, Quaternion.identity); //spawn a bonus at this random place
            go.GetComponent<bonusdeplac>().direction = 1;
            go.GetComponent<bonusdeplac>().bonusEffect = bonusEffect[Random.Range(0, bonusEffect.Length)];

        }
        else if (whereSpawn == 2) //bottom
        {
            screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), -5.5f, 10)); //pick a random place on the bottom of the screen

            GameObject go = Instantiate(bonus, screenPos, Quaternion.identity); //spawn a bonus at this random place
            go.GetComponent<bonusdeplac>().direction = 2;
            go.GetComponent<bonusdeplac>().bonusEffect = bonusEffect[Random.Range(0, bonusEffect.Length)];

        }
        else if (whereSpawn == 3) //right
        {
            screenPos = Camera.main.ScreenToWorldPoint(new Vector3(-9.5f, Random.Range(0, Screen.height), 10)); //pick a random place on the left of the screen

            GameObject go = Instantiate(bonus, screenPos, Quaternion.identity); //spawn a bonus at this random place
            go.GetComponent<bonusdeplac>().direction = 0;
            go.GetComponent<bonusdeplac>().bonusEffect = bonusEffect[Random.Range(0, bonusEffect.Length)];

        }
    }

    IEnumerator EnemySequencer(Vector3 spawnPos)
    {
        Vector3 alertPos = spawnPos + (GameObject.Find("Field").transform.position - spawnPos) * 0.2f;
        GameObject alert = Instantiate(Alert, alertPos, Quaternion.identity);

        yield return new WaitForSeconds(alertDispawnTime);

        Destroy(alert);

        yield return new WaitForSeconds(afterAlertSpawnTime - alertDispawnTime);

        GameObject go = Instantiate(enemys[randomEnemy()], spawnPos, Quaternion.identity); //spawn a random enemy at this random place
        //Nicolas Pupulin
        go.GetComponent<EnemyMissile>().whereSpawn = whereSpawn;
        //..Nicolas Pupulin
        if (enemyToSpawn == 0)
        {
            go.GetComponent<EnemyMissile>().lastOfWave = true;
        }

    }

    IEnumerator WaveAnouncer()
    {
        waveAnounce.SetActive(true);
        for (int i = 0; i < diffModifier; i++)
        {
            enemyIcons[i].SetActive(true);
            enemyIcons[i].GetComponent<Image>().sprite = enemys[i].GetComponent<EnemyMissile>().enemyIcon;
        }
        waveAff.text = int2roman(waveNumber + 1); //"Wave " + int2roman(waveNumber + 1);

        if (start)
        {
            yield return new WaitForSeconds(2);
        }
        else
        {
            yield return new WaitForSeconds(timeBetweenWaveStart);
        }

        waveAff.text = "";
        for (int i = 0; i < diffModifier - 1; i++)
        {
            enemyIcons[i].GetComponent<Image>().sprite = null;
            enemyIcons[i].SetActive(false);
        }
        waveAnounce.SetActive(false);
        if (start)
        {
            start = false;
        }
    }

    string int2roman(int number)
    {
        int[] arabic = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
        string[] roman = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
        string result = "";
        for (int i = 0; i < 13; i++)
        {
            while (number >= arabic[i])
            {
                result = result + roman[i].ToString();
                number = number - arabic[i];
            }
        }
        return result;
    }

    //..MACHADO Julien
}
