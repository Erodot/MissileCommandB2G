using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Laser : Shoot
{
    public GameObject Bullet;
    public int bulletSpeed;
    public GameObject LaserBeam;
    public float laserBeamLifeTime;
    public bool tir = false;
    public GameObject Canon;

    public float reloadTime;
    bool canShoot = true;
    bool shoot;
    public ControlSettings controlSettings;
    public GameManager gameManager;

    public float timer;
    float currentTimer;

    public GameObject fxShoot;

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
                if(Mathf.RoundToInt(controlSettings.Shoot.ReadValue<float>()) == 1)
                {
                    StartCoroutine(ShootFx());
                    
                }
            }
            if (Mathf.RoundToInt(controlSettings.Shoot.ReadValue<float>()) == 0 && shoot)
            {
                shoot = false;
                StartCoroutine(Reload());
            }

            if (gameManager.controlKeyboard)
            {
                if (Keyboard.current.spaceKey.isPressed && isActivated && canShoot)
                {
                    
                    GameObject go = Instantiate(LaserBeam, Canon.transform.position, gameObject.transform.rotation);
                    go.transform.parent = gameObject.transform;
                    StartCoroutine(DestroyLaser(go));
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
        if (gameManager.turretCanShoot && !isActivated && gameManager.startGame && !isDestroy)
        {
            if (currentTimer > 0)
            {
                currentTimer -= Time.deltaTime;
            }
            else if (currentTimer <= 0)
            {
                GameObject go = Instantiate(Bullet, Canon.transform.position, gameObject.transform.rotation);
                go.GetComponent<NewBullet>().direction = Canon.transform.position - transform.position;
                go.GetComponent<NewBullet>().speed = bulletSpeed;

                currentTimer = timer;
            }
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        canShoot = true;
    }

    IEnumerator DestroyLaser(GameObject toDestroy)
    {
        yield return new WaitForSeconds(laserBeamLifeTime);
        Destroy(toDestroy);
    }

    IEnumerator ShootFx()
    {
        shoot = true;
        canShoot = false;
        GameObject fx = Instantiate(fxShoot, Canon.transform.position, Quaternion.identity);
        fx.transform.parent = gameObject.transform;
        yield return new WaitForSeconds(0.3f);
        Destroy(fx);
        GameObject go = Instantiate(LaserBeam, Canon.transform.position, gameObject.transform.rotation);
        go.transform.parent = gameObject.transform;
        StartCoroutine(DestroyLaser(go));
        GetComponent<BuildingLifeDamage>().ManageSound("shoot");
    }
}
