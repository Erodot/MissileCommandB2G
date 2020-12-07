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
    //..Nicolas Pupulin

    GameObject[] primaryTargets;
    List<Transform> finalTargets = new List<Transform>();
    Vector3 target;
    public float speed; //speed of the missile
    [Range(0.0f, 10.0f)]
    public float baseSpeed;
    public string type;

    // Start is called before the first frame update
    void Start()
    {
        //primaryTargets = GameObject.FindGameObjectsWithTag("Player"); //find randomly the target of the missile
        //TestRaycast();
        //target = finalTargets[Random.Range(0, finalTargets.Count)].position; //set this target as a vector 3
        speed = baseSpeed;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        target = FindClosestTarget("Player").transform.position; //find closest target
        gameObject.transform.LookAt(target); //rotate towards his target
    }

    // Update is called once per frame
    void Update()
    {
        GoToTarget();
    }

    void GoToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime); //make the missile move towards the target
    }

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
                    if (other.GetComponent<NewShoot>() != null) //if the building is a turret
                    {
                        other.GetComponent<NewShoot>().enabled = false;
                    }
                    DestroyThis(); //destroy the missile
                }
                else
                {
                    damageIndex = gameManager.BuildingList.IndexOf(other.gameObject);
                }
            }
            else
            {
                gameManager.isShieldActivated = false;
                DestroyThis(); //destroy the missile
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

            DestroyThis(); //destroy the missile
        }

        if (other.gameObject.CompareTag("Explosion")) //if the missile hit a player bullet
        {
            //Nicolas Pupulin
            lifePoint--;
            if (lifePoint == 0)
            {
                //Instantiate(explosion, transform.position, Quaternion.identity);
                DestroyThis(); //destroy the missile
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

    /*void TestRaycast()
    {
        for (int i = 0; i < primaryTargets.Length; i++)
        {
            RaycastHit hit;
            if (Physics.Linecast(transform.position, primaryTargets[i].transform.position, out hit))
            {
                if (hit.transform.tag != "Ground")
                {
                    finalTargets.Add(hit.transform);
                }
            }
        }

    }*/
    //..MACHADO Julien

    //Coline Marchal
    /*public string OnMyLeftOrOnMyRight (Vector3 targetPos)
    {
        Vector3 forward = transform.forward;
        Vector3 up = transform.up;
        Vector3 targetDirection = targetPos - transform.position;

        //-1 = left
        //1 = right
        Vector3 perp = Vector3.Cross(forward, targetDirection);
        float dir = Vector3.Dot(perp, up);
        string toReturn;

        if (dir > 0.0f)
        {
            toReturn = "left";
        }
        else if (dir < 0.0f)
        {
            toReturn = "right";
        }
        else
        {
            toReturn = "fwd";
        }
        //Debug.Log(toReturn);
        return toReturn;
    }



    public void HitSomething(GameObject building)
    {
        if (gameManager != null)
        {
            //gameManager.TurretList.Remove(building);
            gameManager.CheckNeighbour(building);
        }
    }*/

    [ContextMenu("DestroyHive")]
    public void DestroyThis()
    {
        if (type == "hive") //ruche
        {
            //instanciate 5 simple ennemies
            for (int i = 0; i < 5; i++)
            {
                float angle = 360f / 5;
                float rayon = 3f;
                Vector3 newEnnemyPos = new Vector3(transform.position.x + rayon, transform.position.y, transform.position.z + rayon);
                Quaternion q = Quaternion.Euler(0,90,0);
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

        Destroy(this.gameObject);
    }
    //..Coline Marchal
}
