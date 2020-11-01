using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTest2 : MonoBehaviour
{
    //MACHADO Julien

    Vector3 screenPos;

    [SerializeField]
    GameObject Enemy;

    int enemyToSpawn;
    [Range(0, 50)]
    public int enemyMin;
    [Range(0, 50)]
    public int enemyMax;
    [Range(0.0f, 5.0f)]
    public float timeToSpawn;
    float actualTime;



    // Start is called before the first frame update
    void Start()
    {
        enemyToSpawn = Random.Range(enemyMin, enemyMax);
        actualTime = timeToSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyToSpawn > 0)
        {
            if (actualTime == timeToSpawn)
            {
                InstantiateEnemy();
            }

            if (actualTime > 0)
            {
                actualTime -= Time.deltaTime;
            }
            else
            {
                actualTime = timeToSpawn;
            }
        }
    }

    void InstantiateEnemy()
    {
        int whereSpawn;
        whereSpawn = Random.Range(0, 4);
        if(whereSpawn == 0)
        {
            screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), 5.5f, 10));

            GameObject go = Instantiate(Enemy, screenPos, Quaternion.identity);
        }
        else if(whereSpawn == 1)
        {
            screenPos = Camera.main.ScreenToWorldPoint(new Vector3(9.5f, Random.Range(0, Screen.height), 10));

            GameObject go = Instantiate(Enemy, screenPos, Quaternion.identity);
        }
        if (whereSpawn == 2)
        {
            screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), -5.5f, 10));

            GameObject go = Instantiate(Enemy, screenPos, Quaternion.identity);
        }
        else if (whereSpawn == 3)
        {
            screenPos = Camera.main.ScreenToWorldPoint(new Vector3(-9.5f, Random.Range(0, Screen.height), 10));

            GameObject go = Instantiate(Enemy, screenPos, Quaternion.identity);
        }

        enemyToSpawn -= 1;
    }

    //..MACHADO Julien
}
