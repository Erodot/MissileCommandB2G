using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewShoot : MonoBehaviour
{

    public GameObject Bullet;
    public bool tir = false;
    public bool isDestroy;
    public int indexTurret; //Indicate wich turret he is.
    public GameObject Canon;
    public bool isActivated;

    public float reloadTime;
    bool canShoot = true;
    bool shoot = true;
    public ControlSettings controlSettings;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Awake()
    {
        controlSettings = GameObject.Find("ControlManager").GetComponent<ControlSettings>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.turretCanShoot)
        {
            if (isActivated && canShoot)
            {
                if (Mathf.RoundToInt(controlSettings.Shoot.ReadValue<float>()) == 1 && shoot == true)
                {
                    Debug.Log("shoot");
                    GameObject go = Instantiate(Bullet, Canon.transform.position, gameObject.transform.rotation);
                    go.GetComponent<NewBullet>().direction = Canon.transform.position - transform.position;

                    shoot = false;
                    canShoot = false;
                    StartCoroutine(Reload());
                }
                else if (Mathf.RoundToInt(controlSettings.Shoot.ReadValue<float>()) != 1 && shoot == false)
                {
                    shoot = true;
                }
            }

            if (gameManager.controlKeyboard)
            {
                if (Keyboard.current.spaceKey.wasPressedThisFrame && isActivated && canShoot)
                {
                    GameObject go = Instantiate(Bullet, Canon.transform.position, gameObject.transform.rotation);
                    go.GetComponent<NewBullet>().direction = Canon.transform.position - transform.position;

                    canShoot = false;
                    StartCoroutine(Reload());
                }
            }
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        canShoot = true;
    }
}
