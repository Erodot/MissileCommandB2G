using System.Collections;
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

    GameObject[] primaryTargets;
    List<Transform> finalTargets = new List<Transform>();
    Vector3 target;
    public float speed; //speed of the missile
    [Range(0.0f, 10.0f)]
    public float baseSpeed;
    public string type;
    public float weight;
    public float bomberSpawnTimer;  public float bomberRotateDistance;  //bomber
    public Sprite enemyIcon;
    public int explosionRadius;

    // Start is called before the first frame update
    void Start()
    {
        //primaryTargets = GameObject.FindGameObjectsWithTag("Player"); //find randomly the target of the missile
        //TestRaycast();
        //target = finalTargets[Random.Range(0, finalTargets.Count)].position; //set this target as a vector 3
        speed = baseSpeed;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
        float angle = 1f * baseSpeed;

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

                    ShootingZoneTest turret;
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
                DestroyThis(other.gameObject); //destroy the missile
            }
        }

        if (other.gameObject.CompareTag("Ground")) //if the missile hit the ground
        {
            if (!gameManager.isShieldActivated)
            {
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
            }

            DestroyThis(other.gameObject); //destroy the missile
        }

        if (other.gameObject.CompareTag("Bullet") || other.gameObject.CompareTag("Explosion")) //if the missile hit a player bullet
        {
            //Nicolas Pupulin
            lifePoint--;
            if (lifePoint == 0)
            {
                //Instantiate(explosion, transform.position, Quaternion.identity);
                DestroyThis(other.gameObject); //destroy the missile
            }
            //..Nicolas Pupulin
        }
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
            if (tag.CompareTag("Bullet") || tag.CompareTag("Explosion"))
            {
                //instanciate 5 simple ennemies
                for (int i = 0; i < 5; i++)
                {
                    float angle = 360f / 5;
                    float rayon = 1f;
                    Vector3 newEnnemyPos = new Vector3(transform.position.x + rayon, transform.position.y + rayon, transform.position.z);
                    Quaternion q = Quaternion.Euler(0, 90, 0);
                    GameObject nmi = Instantiate(ennemiesPrefab[0], newEnnemyPos, q);
                    Debug.Log(q);

                    //rotation
                    transform.RotateAround(transform.position, Vector3.back, angle);


                    nmi.transform.parent = transform;
                }
                for (int i = 0; i < 5; i++)
                {
                    transform.GetChild(0).transform.parent = null;
                }
            }
        }
        gameManager.silverBulletCount++;
        if (tag.CompareTag("Bullet"))
        {
            GameObject go = Instantiate(explosion, tag.transform.position, Quaternion.identity);
            go.GetComponent<PlayerProjectile_Explosion>().radiusMultiplier = explosionRadius;
        }
        if (tag.CompareTag("Explosion"))
        {
            GameObject go = Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            go.GetComponent<PlayerProjectile_Explosion>().radiusMultiplier = explosionRadius;
        }


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
        speed = speed * speedModifier;
        target = FindClosestTarget("Player").transform.position; //find closest target
        gameObject.transform.LookAt(target); //rotate towards his target
    }
    //..Nicolas Pupulin
}
