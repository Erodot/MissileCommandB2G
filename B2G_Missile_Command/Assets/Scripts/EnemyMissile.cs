using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    //MACHADO Julien

    public GameObject Explosion;
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
        if(other.gameObject.CompareTag("Player")) //if the missile hit a building
        {
            other.gameObject.GetComponent<MeshRenderer>().enabled = false; //deactivate building renderer
            other.gameObject.GetComponent<BoxCollider>().enabled = false; //deactivate building boxcollider
            if(other.gameObject.GetComponent<TurretAllie>() != null) //if the building is a turret
            {
                other.gameObject.GetComponent<TurretAllie>().isDestroy = true; //deactivate his TurretAllie component
            }
            Destroy(this.gameObject); //destroy the missile
        }
        if (other.gameObject.CompareTag("Ground")) //if the missile hit the ground
        {
            Destroy(this.gameObject); //destroy the missile
        }
        if (other.gameObject.CompareTag("Explosion")) //if the missile hit a player bullet
        {
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject); //destroy the missile
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
}
