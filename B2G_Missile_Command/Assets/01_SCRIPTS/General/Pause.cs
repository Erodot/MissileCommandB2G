using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!PauseMenu.activeSelf)
            {
                PauseMenu.SetActive(true);
                Time.timeScale = 0;
                gameManager.turretCanShoot = false;
            }
            else
            {
                PauseMenu.SetActive(false);
                Time.timeScale = 1;
                gameManager.turretCanShoot = true;
            }
        }
    }

    public void Exit()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        gameManager.turretCanShoot = true;
    }
}
