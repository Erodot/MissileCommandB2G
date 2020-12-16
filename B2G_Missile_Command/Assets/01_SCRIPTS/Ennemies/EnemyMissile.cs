﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    //MACHADO Julien

    public GameManager gameManager;
    public GameObject explosion;
    public List<GameObject> ennemiesPrefab = new List<GameObject>();

    int damageIndex = 100;

    //Nicolas Pupulin
    public int lifePoint;

    public Vector3 randomDirection;

    public EnemySpawnTest2 ennemySpawnTest2;

    public int whereSpawn;

    public float speedModifier;

    //..Nicolas Pupulin

    //Corentin SABIAUX GCC2
    public int scoreValue;

    public LevelScoreTest levelScoreTest;
    public bool multiplierIsOnEnemyMissile;
    //..Corentin SABIAUX GCC2

    GameObject[] primaryTargets;
    List<Transform> finalTargets = new List<Transform>();
    Vector3 target;
    public float speed; //speed of the missile
    [Range(0.0f, 10.0f)]
    public float baseSpeed;
    [HideInInspector]
    public float baseVirguleSpeed;
    public string type;
    public float weight;
    public float bomberSpawnTimer;  public float bomberRotateDistance;  //bomber
    public Sprite enemyIcon;
    public int explosionRadius;
    public bool lastOfWave;
    [HideInInspector]
    public bool virguleActivated;

    public Animator animator;
    bool canAnimate = true;

    public GameObject ExplosionFx;
    public float fxLifeTime;

    // Start is called before the first frame update
    void Start()
    {
        //primaryTargets = GameObject.FindGameObjectsWithTag("Player"); //find randomly the target of the missile
        //TestRaycast();
        //target = finalTargets[Random.Range(0, finalTargets.Count)].position; //set this target as a vector 3
        speed = baseSpeed;
        baseVirguleSpeed = baseSpeed * speedModifier;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //Corentin SABIAUX GCC2
        levelScoreTest = GameObject.Find("LevelScore").GetComponent<LevelScoreTest>();
        //..Corentin SABIAUX GCC2

        gameObject.transform.LookAt(target); //rotate towards his target


        //Nicolas Pupulin
        ennemySpawnTest2 = GameObject.Find("Spawner").GetComponent<EnemySpawnTest2>();

        if (whereSpawn == 0)
        {
            randomDirection = new Vector3(Random.Range(-10, 10), 6.5f, transform.position.z);
        } else if (whereSpawn == 1)
        {
            randomDirection = new Vector3(-1, Random.Range(-4, 14), transform.position.z);
        }
        else if (whereSpawn == 2)
        {
            randomDirection = new Vector3(Random.Range(-10, 10), 4, transform.position.z);
        }
        else if (whereSpawn == 3)
        {
            randomDirection = new Vector3(1, Random.Range(-4, 14), transform.position.z);
        }

        if (type == "virgule")
        {
            StartCoroutine(DirectionVirgule());
        } else if (FindClosestTarget("Player") != null)
        {
            target = FindClosestTarget("Player").transform.position; //find closest target
            gameObject.transform.LookAt(target); //rotate towards his target
        }

        //..Nicolas Pupulin
    }

    // Update is called once per frame
    void Update()
    {
        if (type == "bomber")
            RotateAndBomb();
        else
            GoToTarget();
    }

    void GoToTarget()
    {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime); //make the missile move towards the target
    }

    //Coline
    void RotateAndBomb()
    {
        Vector3 planetTransform = GameObject.FindGameObjectWithTag("Ground").transform.position;
        float planetDistance = Vector3.Distance(planetTransform,transform.position);
        float angle = 0.25f * baseSpeed;

        //Debug.Log(planetDistance);

        if(planetDistance < bomberRotateDistance)
        {
            transform.RotateAround(planetTransform, Vector3.forward, angle);
            if(bombSpawn)
            {
                StartCoroutine("BombSpawn");
            }
        }
        else
        {
            if (FindClosestTarget("Player") != null)
            {
                target = FindClosestTarget("Player").transform.position; //find closest target
                gameObject.transform.LookAt(target); //rotate towards his target
            }
            GoToTarget();
        }
    }
    //..Coline

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) //if the missile hit a building
        {
            if (!gameManager.isShieldActivated)
            {
                if (!other.gameObject.GetComponent<BuildingLifeDamage>().destroyed)
                {
                    other.gameObject.GetComponent<BuildingLifeDamage>().Damaged(2);

                    if (other.GetComponent<Shoot>() != null) //if the building is a turret
                    {
                        other.GetComponent<Shoot>().enabled = false;
                    }
                    DestroyThis(other.gameObject); //destroy the missile
                }
                else
                {
                    damageIndex = gameManager.BuildingList.IndexOf(other.gameObject);
                }
            }
            else
            {
                gameManager.isShieldActivated = false;
                GameObject[] shield = GameObject.FindGameObjectsWithTag("Shield");
                for (int i = 0; i < shield.Length; i++)
                {
                    Destroy(shield[i]);
                }
                DestroyThis(other.gameObject); //destroy the missile
            }
        }

        if (other.gameObject.CompareTag("Ground")) //if the missile hit the ground
        {
            if (!gameManager.isShieldActivated)
            {
                GameObject.FindGameObjectWithTag("audio").GetComponent<SoundManager>().Play("planetCollision");
                for (int i = damageIndex; i < gameManager.BuildingList.Count;)
                {
                    if(gameManager.BuildingList[i].GetComponent<BuildingLifeDamage>().destroyed == false)
                    {
                        Debug.Log("Destroy");
                        gameManager.BuildingList[i].GetComponent<BuildingLifeDamage>().Damaged(1);
                        i = gameManager.BuildingList.Count;
                    }
                    else
                    {
                        i = (i + 1) % gameManager.BuildingList.Count;
                    }
                }
                for (int i = damageIndex; i < gameManager.BuildingList.Count;)
                {
                    if (gameManager.BuildingList[i].GetComponent<BuildingLifeDamage>().destroyed == false)
                    {
                        gameManager.BuildingList[i].GetComponent<BuildingLifeDamage>().Damaged(1);
                        i = gameManager.BuildingList.Count;
                    }
                    else
                    {
                        if(i == 0)
                        {
                            i = gameManager.BuildingList.Count-1;
                        }
                        else
                        {
                            i--;
                        }
                    }
                }

                if (gameManager != null && FindClosestTarget("Player") != null)
                {
                    Vector3 closestTarget = FindClosestTarget("Player").transform.position;
                }
            }
            else
            {
                gameManager.isShieldActivated = false;
                GameObject[] shield = GameObject.FindGameObjectsWithTag("Shield");
                for (int i = 0; i < shield.Length; i++)
                {
                    Destroy(shield[i]);
                }
            }

            DestroyThis(other.gameObject); //destroy the missile

        }

        if (other.gameObject.CompareTag("Bullet") || other.gameObject.CompareTag("Explosion") || other.gameObject.CompareTag("Laser")) //if the missile hit a player bullet
        {
            if (other.gameObject.CompareTag("Laser"))
            {
                if(lifePoint > 1)
                {
                    lifePoint -= (lifePoint - 1);
                    SwitchAnim();
                }
                else
                {
                    lifePoint--;
                }
            }
            else
            {
                lifePoint--;
                if(lifePoint <= 1)
                {
                    SwitchAnim();
                }
            }
            //Nicolas Pupulin
            if (lifePoint <= 0)
            {
                //Instantiate(explosion, transform.position, Quaternion.identity);

                //Corentin SABIAUX GCC2
                if (multiplierIsOnEnemyMissile == true)
                {
                    levelScoreTest.MultiplierScore(scoreValue, levelScoreTest.multiplierState);
                } else
                {
                    levelScoreTest.AddScore(scoreValue); //Called the function addScore with scoreValue to add from LevelScoreTest.
                }
                //..Corentin SABIAUX GCC2

                DestroyThis(other.gameObject); //destroy the missile
            }
        }

        //..Nicolas Pupulin
    }

    GameObject FindClosestTarget(string trgt)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag(trgt); //get all the possible target

        GameObject closest = null; //closest target
        float distance = Mathf.Infinity; //closest distance found for now
        Vector3 position = transform.position; //missile position
        foreach (GameObject go in gos) //check in all the possible target
        {
            Vector3 diff = go.transform.position - position; //get the vector difference between the missile and a possible targets
            float curDistance = diff.sqrMagnitude; //change this difference to a float, to get the distance

            if (curDistance < distance) //check if the current distance is smaller than the last smallest
            {
                closest = go; //put this possible targets as the closest
                distance = curDistance; //change the closest distance to the current smallest one
            }
        }

        return closest; //return the closest target
    }


    //[ContextMenu("DestroyHive")]
    public void DestroyThis(GameObject tag)
    {
        if (type == "hive") //ruche
        {
            if (tag.CompareTag("Bullet") || tag.CompareTag("Explosion") || tag.CompareTag("Laser"))
            {
                
                //instanciate 5 simple ennemies
                for (int i = 0; i < 5; i++)
                {
                    float angle = 360f / 5;
                    float rayon = 1f;
                    Vector3 newEnnemyPos = new Vector3(transform.position.x + rayon, transform.position.y + rayon, transform.position.z);
                    Quaternion q = Quaternion.Euler(0, 90, 0);
                    GameObject nmi = Instantiate(ennemiesPrefab[i], newEnnemyPos, q);
                    Debug.Log(q);

                    //rotation
                    transform.RotateAround(transform.position, Vector3.back, angle);


                    nmi.transform.parent = gameObject.transform;
                }
                for (int i = 0; i < 5; i++)
                {
                    transform.GetChild(1).transform.parent = null;
                }
            }
        }
        gameManager.silverBulletCount++;
        if (tag.CompareTag("Bullet"))
        {
            GameObject fx = Instantiate(ExplosionFx, tag.transform.position, Quaternion.identity);
            GameObject go = Instantiate(explosion, tag.transform.position, Quaternion.identity);
            fx.transform.parent = go.transform; 
            go.GetComponent<PlayerProjectile_Explosion>().radiusMultiplier = explosionRadius;
        }
        else if (tag.CompareTag("Explosion"))
        {
            GameObject fx = Instantiate(ExplosionFx, tag.transform.position, Quaternion.identity);
            GameObject go = Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            fx.transform.parent = go.transform;
            go.GetComponent<PlayerProjectile_Explosion>().radiusMultiplier = explosionRadius;
        }
        else if (tag.CompareTag("Laser"))
        {
            GameObject fx = Instantiate(ExplosionFx, gameObject.transform.position, Quaternion.identity);
            GameObject go = Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            fx.transform.parent = go.transform;
            go.GetComponent<PlayerProjectile_Explosion>().radiusMultiplier = explosionRadius;
        }

        if (lastOfWave)
        {
            GameObject.Find("Spawner").GetComponent<EnemySpawnTest2>().pacingStart = true;
        }

        gameManager.enemyKill[type] += 1;
        //Debug.Log(gameObject.name);

        GameObject.FindGameObjectWithTag("audio").GetComponent<SoundManager>().Play("deathEnnemy");
        Destroy(this.gameObject);
    }

    bool bombSpawn= true;
    IEnumerator BombSpawn()
    {
        bombSpawn = false;
        yield return new WaitForSeconds(bomberSpawnTimer);
        GameObject nmi = Instantiate(ennemiesPrefab[0], transform.position, Quaternion.identity);
        bombSpawn = true;
    }
    //..Coline Marchal

    //Nicolas Pupulin
    IEnumerator DirectionVirgule()
    {
        target = randomDirection;
        gameObject.transform.LookAt(target); //rotate towards his target
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime); //make the missile move towards a random direction
        yield return new WaitForSeconds(2);
        virguleActivated = true;
        speed = speed * speedModifier;
        target = FindClosestTarget("Player").transform.position; //find closest target
        gameObject.transform.LookAt(target); //rotate towards his target
    }
    //..Nicolas Pupulin

    //Julien MACHADO
    void SwitchAnim()
    {
        if(animator != null && canAnimate)
        {
            animator.SetBool("hasArmor", false);
            canAnimate = false;
        }
    }
    //..Julien MACHADO
}
