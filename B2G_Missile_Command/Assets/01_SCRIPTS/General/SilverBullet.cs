using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class SilverBullet : MonoBehaviour
{
    public ControlSettings controlSettings;
    public GameManager gameManager;
    public EnemySpawnTest2 enemySpawn;
    public LevelScoreTest levelScore;

    public GameObject Arcana;

    bool destroyEnemys;
    bool isActivate;

    //Corentin SABIAUX GCC2
    public bool multiplierIsOnSilverBullet;
    //..Corentin SABIAUX GCC2

    // Start is called before the first frame update
    void Start()
    {
        controlSettings = GameObject.Find("ControlManager").GetComponent<ControlSettings>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemySpawn = GameObject.Find("Spawner").GetComponent<EnemySpawnTest2>();
        levelScore = GameObject.Find("LevelScore").GetComponent<LevelScoreTest>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.RoundToInt(controlSettings.SilverBullet.ReadValue<float>()) == 1 && !isActivate)
        {
            GameObject go = Instantiate(Arcana, new Vector3(0, 5.25f, 0), Quaternion.identity);
            destroyEnemys = true;
            StartCoroutine(DestroyArcana(go));
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

                //Corentin SABIAUX GCC2
                if (multiplierIsOnSilverBullet == true)
                {
                    levelScore.MultiplierScore(enemys[i].GetComponent<EnemyMissile>().scoreValue, levelScore.multiplierState);
                } else
                {
                    levelScore.AddScore(enemys[i].GetComponent<EnemyMissile>().scoreValue);
                }
                //..Corentin SABIAUX GCC2

                Destroy(enemys[i]);
            }
        }
    }

    IEnumerator DestroyArcana(GameObject arcana)
    {
        //gameManager.silverBulletText.SetActive(false);
        Destroy(GameObject.Find("ArcanaChargedV1(Clone)"));
        gameManager.silverBulletCount = 0;
        yield return new WaitForSeconds(3);
        Destroy(arcana);
        Destroy(gameObject);

    }

}
