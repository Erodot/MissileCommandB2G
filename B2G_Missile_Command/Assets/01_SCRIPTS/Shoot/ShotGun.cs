using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : MonoBehaviour
{
    public GameObject Bullet;
    public int bulletSpeed;
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
            else if (Mathf.RoundToInt(controlSettings.Shoot.ReadValue<float>()) == 0 && shoot)
            {
                if(timeHeld < timeToHold / 4)
                {
                    Debug.Log("normal shoot");
                    SpawnBullet(Canon.transform.position, transform.position);
                }
                else if(timeHeld < 2*(timeToHold / 4))
                {
                    Debug.Log("normal shoot2");
                    SpawnBullet(Canon.transform.position, (transform.localPosition + new Vector3(0.15f, 0, 0)));
                    SpawnBullet(Canon.transform.position, (transform.localPosition + new Vector3(-0.15f, 0, 0)));
                }
                else if (timeHeld < 3 * (timeToHold / 4))
                {
                    Debug.Log("normal shoot2");
                    SpawnBullet(Canon.transform.position, transform.position);
                    SpawnBullet(Canon.transform.position, (transform.localPosition + new Vector3(0.25f, 0, 0)));
                    SpawnBullet(Canon.transform.position, (transform.localPosition + new Vector3(-0.25f, 0, 0)));
                }
                else if (timeHeld < timeToHold)
                {
                    Debug.Log("normal shoot2");
                    SpawnBullet(Canon.transform.position, (transform.localPosition + new Vector3(0.15f, 0, 0)));
                    SpawnBullet(Canon.transform.position, (transform.localPosition + new Vector3(-0.15f, 0, 0)));
                    SpawnBullet(Canon.transform.position, (transform.localPosition + new Vector3(0.55f, 0, 0)));
                    SpawnBullet(Canon.transform.position, (transform.localPosition + new Vector3(-0.55f, 0, 0)));
                }



                timeHeld = 0;
                shoot = false;
                canShoot = false;
                StartCoroutine(Reload());

            }
        }

        void SpawnBullet(Vector3 vector1, Vector3 vector2)
        {
            GameObject go = Instantiate(Bullet, Canon.transform.position, gameObject.transform.rotation);
            go.GetComponent<NewBullet>().direction = vector1 - vector2;
            go.GetComponent<NewBullet>().speed = bulletSpeed;
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
