using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewShoot : MonoBehaviour
{

    public GameObject Bullet;
    public bool tir = false;
    public bool isDestroy;
    public int indexTurret; //Indicate wich turret he is.
    public GameObject Canon;
    public bool isActivated;

    bool shoot = true;
    public ControlSettings controlSettings;

    // Start is called before the first frame update
    void Awake()
    {
        controlSettings = GameObject.Find("ControlManager").GetComponent<ControlSettings>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated)
        {
            if (Mathf.RoundToInt(controlSettings.Shoot.ReadValue<float>()) == 1 && shoot == true)
            {
                Debug.Log("shoot");
                GameObject go = Instantiate(Bullet, Canon.transform.position, gameObject.transform.rotation);
                go.GetComponent<NewBullet>().direction = Canon.transform.position - transform.position;

                shoot = false;
            }
            else if (Mathf.RoundToInt(controlSettings.Shoot.ReadValue<float>()) != 1 && shoot == false)
            {
                shoot = true;
            }
        }
    }
}
