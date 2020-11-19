using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingZoneTest : MonoBehaviour
{
    //MACHADO Julien
    public GameObject Bullet;
    public Vector3 groundPosition;//planet
    public Vector3 mouseDirection;
    public Vector3 shootDirection;
    public Camera mainCamera;
    public bool tir = false;
    public bool isDestroy;
    //..MACHADO Julien

    //Corentin SABIAUX GCC2
    public GameManager gameManager; //We need to stock the gameManager of the scene.
    public int indexTurret; //Indicate wich turret he is.
    
    private void OnMouseDown()
    {
        Vector3 canonPosition = transform.parent.position; //where do you shoot from
        if (gameManager.turretCanShoot == true) //If the turrets can shoot.
        {
            //..Corentin SABIAUX GCC2

            //MACHADO Julien
            mouseDirection = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -mainCamera.transform.position.z));
            mouseDirection -= canonPosition;
            shootDirection = mouseDirection;

            //..MACHADO Julien

            //Corentin SABIAUX GCC2
            StartCoroutine(gameManager.CoolDown()); //Start the coroutine CoolDown() present into the gameManager.
            //..Corentin SABIAUX GCC2

            //MACHADO Julien
            if (!isDestroy)
            {
                GameObject bullet;

                bullet = GameObject.Instantiate(Bullet, canonPosition, Quaternion.identity);

                bullet.GetComponent<PlayerMissile>().direction = new Vector3(shootDirection.x * Time.deltaTime, shootDirection.y * Time.deltaTime, 0);
                tir = true;
            }
            else
            {
                tir = false;
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        //..MACHADO Julien

        //Corentin SABIAUX GCC2
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); //We stock the gameManager of the scene.

        //gameObject.GetComponent<PolygonCollider2D>().SetPath(0, gameManager.listOfTurrets.listTurretZone[indexTurret].pointsTurretZone); 
        //We set the PolygonCollider points of the turret by the points included into the 3 lists nested on gameManager.
        //..Corentin SABIAUX GCC2
        /// >> I change manually the polygon on another prefab, so this was a problem to keep the new zoning (i didn't know how to change easily the points, so I thought changin the polygon directly was maybe a best way
        /// >> Coline Marchal
    }

    //MACHADO Julien
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
    //..MACHADO Julien
}
