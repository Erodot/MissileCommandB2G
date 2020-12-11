using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class SilverBullet : MonoBehaviour
{
    public ControlSettings controlSettings;
    public GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        controlSettings = GameObject.Find("ControlManager").GetComponent<ControlSettings>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.RoundToInt(controlSettings.SilverBullet.ReadValue<float>()) == 1)
        {
            GameObject[] enemys = GameObject.FindGameObjectsWithTag("Ennemy");
            for (int i = 0; i < enemys.Length; i++)
            {
                Destroy(enemys[i]);
            }
            gameManager.silverBulletText.SetActive(false);
            gameManager.silverBulletCount = 0;
            Destroy(gameObject);
        }
    }


}
