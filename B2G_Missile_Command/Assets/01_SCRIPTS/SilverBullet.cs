using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class SilverBullet : MonoBehaviour
{
    public ControlSettings controlSettings;
    public GameManager gameManager;
    public EnemySpawnTest2 enemySpawn;
    public GameObject Arcana;

    bool destroyEnemys;
    bool isActivate;

    // Start is called before the first frame update
    void Start()
    {
        controlSettings = GameObject.Find("ControlManager").GetComponent<ControlSettings>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemySpawn = GameObject.Find("Spawner").GetComponent<EnemySpawnTest2>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.RoundToInt(controlSettings.SilverBullet.ReadValue<float>()) == 1 && !isActivate)
        {
            GameObject go = Instantiate(Arcana, new Vector3(0, 5.25f, 0), Quaternion.identity);
            destroyEnemys = true;
            StartCoroutine(DestroyArcana());
            isActivate = true;
        }
        if (destroyEnemys)
        {
            GameObject[] enemys = GameObject.FindGameObjectsWithTag("Ennemy");
            for (int i = 0; i < enemys.Length; i++)
            {
                if (enemys[i].GetComponent<EnemyMissile>().lastOfWave)
                {
                    enemySpawn.pacingStart = true;
                }
                Destroy(enemys[i]);
            }
        }
    }

    IEnumerator DestroyArcana()
    {
        gameManager.silverBulletText.SetActive(false);
        gameManager.silverBulletCount = 0;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);

    }

}
