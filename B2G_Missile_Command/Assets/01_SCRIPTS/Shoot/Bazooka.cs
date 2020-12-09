using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazooka : MonoBehaviour
{
    public GameObject Bullet;
    public int bulletSpeed;
    public int radiusMultiplication;
    public bool tir = false;
    public bool isDestroy;
    public int indexTurret; //Indicate wich turret he is.
    public GameObject Canon;
    public bool isActivated;

    public float reloadTime;
    bool canShoot = true;
    bool shoot;
    public ControlSettings controlSettings;
    public GameManager gameManager;

    public float timeToHold;
    public float timeHeld;

    // Start is called before the first frame update
    void Awake()
    {
        controlSettings = GameObject.Find("ControlManager").GetComponent<ControlSettings>();
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (gameManager.turretCanShoot)
        //{
        if (isActivated && canShoot)
        {
            if (Mathf.RoundToInt(controlSettings.Shoot.ReadValue<float>()) == 1 && canShoot)
            {
                timeHeld += Time.deltaTime;
                shoot = true;
            }
            if (Mathf.RoundToInt(controlSettings.Shoot.ReadValue<float>()) == 0 && shoot)
            {
                Debug.Log("normal shoot");
                GameObject go = Instantiate(Bullet, Canon.transform.position, gameObject.transform.rotation);
                go.GetComponent<NewBullet>().direction = Canon.transform.position - transform.position;
                go.GetComponent<NewBullet>().speed = bulletSpeed;
                go.GetComponent<NewBullet>().explosionRadius += timeHeld * radiusMultiplication;
                go.GetComponent<NewBullet>().direction = Canon.transform.position - transform.position;


                timeHeld = 0;
                shoot = false;
                canShoot = false;
                StartCoroutine(Reload());

            }
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
        //}
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        canShoot = true;
    }
}
