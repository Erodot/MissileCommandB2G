using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewShoot : MonoBehaviour
{

    public GameObject Bullet;
    public Vector3 groundPosition;//planet
    public Vector3 mouseDirection;
    public Vector3 shootDirection;
    public Camera mainCamera;
    public bool tir = false;
    public bool isDestroy;
    public int indexTurret; //Indicate wich turret he is.
    public GameObject Canon;
    public bool isActivated;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject go = Instantiate(Bullet, Canon.transform.position, gameObject.transform.rotation);
                go.GetComponent<NewBullet>().direction = Canon.transform.position - transform.position;
            }
        }
    }
}
