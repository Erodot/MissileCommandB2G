using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAllie : MonoBehaviour
{
    public GameObject Bullet;
    public Vector3 mouseDirection;
    public Vector3 shootDirection;
    public float distance;
    public Camera mainCamera;
    public bool tir = false;
    public bool isDestroy;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    public virtual void Shoot()
    {
        mouseDirection = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -mainCamera.transform.position.z));
        mouseDirection -= transform.position;
        shootDirection = mouseDirection;


        if (Input.GetMouseButtonDown(0) && !isDestroy)
        {
            GameObject bullet;

            bullet = GameObject.Instantiate(Bullet, transform.position, Quaternion.identity);

            bullet.GetComponent<PlayerMissile>().direction = new Vector3(shootDirection.x * Time.deltaTime, shootDirection.y * Time.deltaTime, 0);
            tir = true;
        }
        else
        {
            tir = false;
        }
    }


    public virtual void lancePierreOrientation()
    {
        float zRotation = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, zRotation - 90));
    }

}
