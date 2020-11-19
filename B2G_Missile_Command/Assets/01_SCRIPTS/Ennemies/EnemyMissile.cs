using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    //MACHADO Julien

    public GameManager gameManager;
    public GameObject Explosion;

    int damageIndex = 100;

    //Nicolas Pupulin
    public int lifePoint;
    //..Nicolas Pupulin

    [Range(0, 5)]
    public int ennemyDifficulty;

    GameObject[] primaryTargets;
    List<Transform> finalTargets = new List<Transform>();
    Vector3 target;
    [Range(0.0f, 10.0f)]
    public float speed; //speed of the missile

    // Start is called before the first frame update
    void Start()
    {
        //primaryTargets = GameObject.FindGameObjectsWithTag("Player"); //find randomly the target of the missile
        //TestRaycast();
        //target = finalTargets[Random.Range(0, finalTargets.Count)].position; //set this target as a vector 3
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        target = FindClosestTarget("Player").transform.position; //find closest target
        gameObject.transform.LookAt(target); //rotate towards his target
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime); //make the missile move towards the target
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) //if the missile hit a building
        {
            if (other.gameObject.GetComponent<MeshRenderer>().enabled == true)
            {
                other.gameObject.GetComponent<BuildingLifeDamage>().Damaged(2);

                ShootingZoneTest turret;
                if (other.gameObject.transform.Find("Zone")) //if the building is a turret
                {
                    turret = other.gameObject.transform.Find("Zone").gameObject.GetComponent<ShootingZoneTest>();
                    turret.isDestroy = true; //deactivate his ShootingZoneTest component

                    if (gameManager != null)
                    {
                        gameManager.ShootingZoneList.Remove(turret);
                    }
                }
                Destroy(this.gameObject); //destroy the missile
            }
            else
            {
                damageIndex = gameManager.BuildingList.IndexOf(other.gameObject);
            }
        }

        if (other.gameObject.CompareTag("Ground")) //if the missile hit the ground
        {
            if(damageIndex != 100)
            {
                if (damageIndex == 0)
                {
                    gameManager.BuildingList[damageIndex + 1].GetComponent<BuildingLifeDamage>().Damaged(1);
                    gameManager.BuildingList[gameManager.BuildingList.Count - 1].GetComponent<BuildingLifeDamage>().Damaged(1);
                }
                else if (damageIndex == gameManager.BuildingList.Count - 1)
                {
                    gameManager.BuildingList[0].GetComponent<BuildingLifeDamage>().Damaged(1);
                    gameManager.BuildingList[damageIndex - 1].GetComponent<BuildingLifeDamage>().Damaged(1);
                }
                else
                {
                    gameManager.BuildingList[damageIndex + 1].GetComponent<BuildingLifeDamage>().Damaged(1);
                    gameManager.BuildingList[damageIndex - 1].GetComponent<BuildingLifeDamage>().Damaged(1);
                }
                damageIndex = 100;
            }

            if (gameManager != null && FindClosestTarget("Player") != null)
            {
                Vector3 closestTarget = FindClosestTarget("Player").transform.position;

                if(Vector3.Distance(closestTarget, transform.position) < 2f)
                {
                    //gameManager.CheckNeighbour(FindClosestTarget("Player"), OnMyLeftOrOnMyRight(closestTarget));
                }
            }

            Destroy(this.gameObject); //destroy the missile
        }

        if (other.gameObject.CompareTag("Explosion")) //if the missile hit a player bullet
        {
            //Nicolas Pupulin
            lifePoint--;
            if (lifePoint == 0)
            {
                Instantiate(Explosion, transform.position, Quaternion.identity);
                Destroy(this.gameObject); //destroy the missile
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

    //..Coline Marchal

}
