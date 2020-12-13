using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class ShotGun : Shoot
{
    public GameObject Bullet;
    public GameObject autoBullet;
    public int bulletSpeed;
    public bool tir = false;
    public GameObject Canon;
    public GameObject[] dispersions;

    public float reloadTime;
    bool canShoot = true;
    bool shoot;
    public ControlSettings controlSettings;
    public GameManager gameManager;

    public float timer;
    float currentTimer;

    public GameObject fxShoot;
    public GameObject fxBullet;

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
                    Debug.Log("normal shoot2");
                    StartCoroutine(ShootFx());
                    SpawnBullet(Canon.transform.position, transform.position);
                    for (int i = 0; i < dispersions.Length; i++)
                    {
                        SpawnBullet(Canon.transform.position, dispersions[i].transform.position);
                    }
                    canShoot = false;
                    shoot = true;
                    GetComponent<BuildingLifeDamage>().ManageSound("shoot");
                }
            }
            else if (Mathf.RoundToInt(controlSettings.Shoot.ReadValue<float>()) == 0 && shoot)
            {
                shoot = false;
                StartCoroutine(Reload());
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
                GameObject go = Instantiate(autoBullet, Canon.transform.position, gameObject.transform.rotation);
                go.GetComponent<NewBullet>().direction = Canon.transform.position - transform.position;
                go.GetComponent<NewBullet>().speed = bulletSpeed;

                currentTimer = timer;
            }
        }

        if (gameManager.controlKeyboard)
        {
            if (Keyboard.current.spaceKey.isPressed && isActivated && canShoot)
            {
                Debug.Log("normal shoot2");
                SpawnBullet(Canon.transform.position, transform.position);
                for (int i = 0; i < dispersions.Length; i++)
                {
                    SpawnBullet(Canon.transform.position, dispersions[i].transform.position);
                }
                canShoot = false;
                shoot = true;
                GetComponent<BuildingLifeDamage>().ManageSound("shoot");
            }
            else if (Keyboard.current.spaceKey.wasReleasedThisFrame && shoot)
            {
                shoot = false;
                StartCoroutine(Reload());
            }
        }
        
    }

    void SpawnBullet(Vector3 vector1, Vector3 vector2)
    {
        GameObject go = Instantiate(Bullet, Canon.transform.position, gameObject.transform.rotation);
        go.GetComponent<NewBullet>().direction = vector1 - vector2;
        go.GetComponent<NewBullet>().speed = bulletSpeed;
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        canShoot = true;
    }

    IEnumerator ShootFx()
    {
        GameObject fx = Instantiate(fxShoot, Canon.transform.position, Quaternion.identity);
        fx.transform.parent = gameObject.transform;
        yield return new WaitForSeconds(0.5f);
        Destroy(fx);
    }
}
