using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public GameObject Turret1;
    public GameObject Turret2;
    public GameObject Turret3;

    GameObject LastActivated;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Turret1.GetComponent<NewShoot>().isActivated = true;
            if(LastActivated != null)
            {
                LastActivated.GetComponent<NewShoot>().isActivated = false;
            }
            LastActivated = Turret1;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Turret2.GetComponent<NewShoot>().isActivated = true;
            if (LastActivated != null)
            {
                LastActivated.GetComponent<NewShoot>().isActivated = false;
            }
            LastActivated = Turret2;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Turret3.GetComponent<NewShoot>().isActivated = true;
            if (LastActivated != null)
            {
                LastActivated.GetComponent<NewShoot>().isActivated = false;
            }
            LastActivated = Turret3;
        }
    }


}
