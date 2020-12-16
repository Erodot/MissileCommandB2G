using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiScore : BonusEffect
{
    //Script by Corentin SABIAUX GCC2, don't hesitate to ask some questions.
    bool startEffect;

    SilverBullet silverBullet;
    LevelScoreTest levelScore;

    public GameObject fxMulti;
    GameObject fxCurrent;
    public GameObject multiScore;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.Find("SilverBullet") != null)
        {
            silverBullet = GameObject.Find("SilverBullet").GetComponent<SilverBullet>();
        }
        levelScore = GameObject.Find("LevelScore").GetComponent<LevelScoreTest>();
        fxCurrent = Instantiate(fxMulti, effectPos, Quaternion.identity);

        multiScore = GameObject.Find("MultiScoreScene");
        Effect();
    }

    // Update is called once per frame
    public override void Update()
    {
        if(startEffect)
        {
            GameObject[] enemys = GameObject.FindGameObjectsWithTag("Ennemy");
            for (int i = 0; i < enemys.Length; i++)
            {
                enemys[i].GetComponent<EnemyMissile>().multiplierIsOnEnemyMissile = true;
            }
        }

        fxCurrent.transform.position = Vector3.MoveTowards(fxCurrent.transform.position, multiScore.transform.position, speed * Time.deltaTime);

        base.Update();
    }

    public override void Effect()
    {
        base.Effect();

        startEffect = true;
        levelScore.multiplierScore.text = "x" + levelScore.multiplierState;
        levelScore.multiplierState++;
        if (silverBullet != null)
        {
            silverBullet.multiplierIsOnSilverBullet = true;
        }
    }

    public override void AfterTimerEffect()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Ennemy");
        if(enemys.Length < 2)
        {
            for (int i = 0; i < enemys.Length; i++)
            {
                enemys[i].GetComponent<EnemyMissile>().multiplierIsOnEnemyMissile = false;
            }
            if (silverBullet != null)
            {
                silverBullet.multiplierIsOnSilverBullet = false;
            }
            levelScore.multiplierScore.text = "";

        }

        startEffect = false;

        Destroy(fxCurrent);
        base.AfterTimerEffect();
    }
    //..Corentin SABIAUX GCC2
}
