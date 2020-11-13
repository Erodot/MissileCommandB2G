using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingZoneTest : MonoBehaviour
{
    //MACHADO Julien
    private void OnMouseDown()
    {
        if(Time.timeScale == 1)
        {
            mouseDirection = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -mainCamera.transform.position.z));
            mouseDirection -= transform.position;
            shootDirection = mouseDirection;


            if (!isDestroy)
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

    }
    //..MACHADO Julien

    public GameObject Bullet;
    public Vector3 groundPosition;//planet
    public Vector3 mouseDirection;
    public Vector3 shootDirection;
    public float distance;
    public Camera mainCamera;
    public bool tir = false;
    public bool canShoot = false;
    public bool isDestroy;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        //groundPosition = transform.parent.transform.parent.transform.parent.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if (MyZone())
        //{
        //Shoot();
        //}
    }

    /*/Coline Marchal
    public bool MyZone()
    {
        //unfinished, useless for now
        float rayon = 100f;
        float angle3 = 360f / 3f; //divided by zones number

        Vector3 limitPos = new Vector3(groundPosition.x + rayon, groundPosition.y, groundPosition.z + rayon);
        //transform.RotateAround(groundPosition, Vector3.up, angle3); /rotation

        
        //Vector3 limit1;
        //Vector3 limit2;
        //Vector3 limit3;
        

        GameObject newLimit = new GameObject();
        newLimit.transform.position = transform.position;
        newLimit.transform.RotateAround(groundPosition, Vector3.up, angle3 / 2f);

        //Vector3 target = newLimit.transform.position;

        //Vector3 target = newLimit.transform.position;
        //target.

        //Vector3 directionTest = (target - groundPosition).normalized;
        //Ray rayTest = new Ray(groundPosition,directionTest);


        mouseDirection = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -mainCamera.transform.position.z));

        Vector2 v1 = new Vector2(groundPosition.x, groundPosition.z);
        Vector2 v2 = new Vector2(mouseDirection.x, mouseDirection.y);

        float posAngle;
        posAngle = Vector2.SignedAngle(v1, v2);
        ///posAngle = Vector2.Angle(v1, v2);

        //Debug.Log(v1);
        //Debug.Log(v2);
        Debug.Log(angle3);
        Debug.Log(posAngle);



        return true;
    }
    //end Coline Marchal*/

    /*public virtual void Shoot()
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
    }*/


    public virtual void Rotation()
    {
        float zRotation = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, zRotation - 90));
    }

    public void Destroy()
    {
        //never called
        isDestroy = true;

    }

    IEnumerator CoolDown()
    {
        canShoot = false;
        float coolDown = 0.2f; //wait before another shoot
        yield return new WaitForSeconds(coolDown);
        canShoot = true;
    }
}
