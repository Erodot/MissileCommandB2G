using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Bazooka : Shoot
{
    public GameObject Bullet;
    public int bulletSpeed;
    public int bulletAutoSpeed;
    public int radiusMultiplication;
    public bool tir = false;
    public GameObject Canon;

    public float reloadTime;
    bool canShoot = true;
    bool shoot;
    public ControlSettings controlSettings;
    public GameManager gameManager;

    public float timer;
    float currentTimer;

    public 

    // Start is called before the first frame update
    void Awake()
    {
        controlSettings = GameObject.Find("ControlManager").GetComponent<ControlSettings>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.turretCanShoot && isActivated && gameManager.startGame && !isDestroy)
        {
            if (canShoot)
            {
                if (Mathf.RoundToInt(controlSettings.Shoot.ReadValue<float>()) == 1)
                {
                    GameObject go = Instantiate(Bullet, Canon.transform.position, gameObject.transform.rotation);
                    go.GetComponent<NewBullet>().direction = Canon.transform.position - transform.position;
                    go.GetComponent<NewBullet>().speed = bulletSpeed;
                    go.GetComponent<NewBullet>().explosionRadius = radiusMultiplication;
                    go.GetComponent<NewBullet>().canExplode = true;
                    go.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    shoot = true;
                    canShoot = false;
                    GetComponent<BuildingLifeDamage>().ManageSound("shoot");
                }
            }
            if (Mathf.RoundToInt(controlSettings.Shoot.ReadValue<float>()) == 0 && shoot)
            {
                shoot = false;
                StartCoroutine(Reload());
            }
        }
        if(gameManager.turretCanShoot && !isActivated && gameManager.startGame && !isDestroy)
        {
            if(currentTimer > 0)
            {
                currentTimer -= Time.deltaTime;
            }
            else if(currentTimer <= 0)
            {
                GameObject go = Instantiate(Bullet, Canon.transform.position, gameObject.transform.rotation);
                go.GetComponent<NewBullet>().direction = Canon.transform.position - transform.position;
                go.GetComponent<NewBullet>().speed = bulletAutoSpeed;

                currentTimer = timer;
            }
        }

        if (gameManager.controlKeyboard)
        {
            if (Keyboard.current.spaceKey.isPressed && isActivated && canShoot)
            {
                GameObject go = Instantiate(Bullet, Canon.transform.position, gameObject.transform.rotation);
                go.GetComponent<NewBullet>().direction = Canon.transform.position - transform.position;
                go.GetComponent<NewBullet>().speed = bulletSpeed;
                go.GetComponent<NewBullet>().explosionRadius = radiusMultiplication;
                go.GetComponent<NewBullet>().canExplode = true;
                shoot = true;
                canShoot = false;
                GetComponent<BuildingLifeDamage>().ManageSound("shoot");
            }
            if (Keyboard.current.spaceKey.wasReleasedThisFrame && shoot)
            {
                shoot = false;
                StartCoroutine(Reload());
            }
        }

    }
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        canShoot = true;
    }
}

