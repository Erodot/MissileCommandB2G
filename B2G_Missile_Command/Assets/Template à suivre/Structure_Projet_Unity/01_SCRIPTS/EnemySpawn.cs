using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    //MACHADO Julien

    Vector3 screenPos;

    [SerializeField]
    GameObject Enemy;

    int enemyToSpawn; //number of enemy to spawn
    [Range(0, 50)]
    public int enemyMin; //number min of enemy to spawn
    [Range(0, 50)]
    public int enemyMax; //number max of enemy to spawn
    [Range(0.0f, 5.0f)]
    public float timeToSpawn; //time between the enemy spawn
    float actualTime; //actual time of the clock



    // Start is called before the first frame update
    void Start()
    {
        enemyToSpawn = Random.Range(enemyMin, enemyMax); //choose the number of enemy to spawn between enemyMin and enemyMax
        actualTime = timeToSpawn; //set the actual time to to the time chosen for the clock
    }

    // Update is called once per frame
    void Update()
    {
        //clock for the enemy spawning
        if(enemyToSpawn > 0) //while there's still enemy to spawn
        {
            if(actualTime == timeToSpawn) //if the actual time is the time to spawn enemy
            {
                InstantiateEnemy(); //spawn an enemy
            }

            if(actualTime > 0) //if the time has'nt reached 0
            {
                actualTime -= Time.deltaTime; //make it decrease
            }
            else 
            {
                actualTime = timeToSpawn; //reset the clock
            }
        }
    }

    void InstantiateEnemy()
    {
        screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0,Screen.width), Screen.height, 10)); //pick a random place on the top of the screen

        GameObject go = Instantiate(Enemy, screenPos, Quaternion.identity); //spawn an enemy at this random place

        enemyToSpawn -= 1; //decrease the enemy spawn counter
    }

    //..MACHADO Julien
}
