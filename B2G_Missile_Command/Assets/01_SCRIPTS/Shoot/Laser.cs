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

    // Start is called before the first frame update
    void Awake()
    {
        controlSettings = GameObject.Find("ControlManager").GetComponent<ControlSettings>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.turretCanShoot && isActivated)
        {
            if (canShoot)
            {
                if(Mathf.RoundToInt(controlSettings.Shoot.ReadValue<float>()) == 1)
                {
                    GameObject go = Instantiate(LaserBeam, Canon.transform.position, gameObject.transform.rotation);
                    go.transform.parent = gameObject.transform;
                    StartCoroutine(DestroyLaser(go));
                    StartCoroutine(Reload());
                    shoot = true;
                    canShoot = false;
                }
            }
            if (Mathf.RoundToInt(controlSettings.Shoot.ReadValue<float>()) == 0 && shoot)
            {
                shoot = false;
                StartCoroutine(Reload());
            }

            /*if (gameManager.controlKeyboard)
            {
                if (Keyboard.current.spaceKey.wasPressedThisFrame && isActivated && canShoot)
                {
                    GameObject go = Instantiate(Bullet, Canon.transform.position, gameObject.transform.rotation);
                    go.GetComponent<NewBullet>().direction = Canon.transform.position - transform.position;

                    canShoot = false;
                    StartCoroutine(Reload());
                }
            }*/
        }
        else if (gameManager.turretCanShoot && !isActivated)
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
}
