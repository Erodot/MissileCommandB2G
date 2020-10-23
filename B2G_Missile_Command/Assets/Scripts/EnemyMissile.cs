using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    //MACHADO Julien

    GameObject[] targets;
    Transform target;
    [Range(0.0f, 10.0f)]
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        targets = GameObject.FindGameObjectsWithTag("Player");
        target = targets[Random.Range(0, targets.Length)].transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            if(other.gameObject.GetComponent<TurretAllie>() != null)
            {
                other.gameObject.GetComponent<TurretAllie>().isDestroy = true;
            }
            Destroy(this.gameObject);
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }

    //..MACHADO Julien
}
