using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiScore : BonusEffect
{
    //Script by Corentin SABIAUX GCC2, don't hesitate to ask some questions.
    bool startEffect;

    SilverBullet silverBullet;
    LevelScoreTest levelScore;

    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.Find("SilverBullet") != null)
        {
            silverBullet = GameObject.Find("SilverBullet").GetComponent<SilverBullet>();
        }
        levelScore = GameObject.Find("LevelScore").GetComponent<LevelScoreTest>();
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

        base.Update();
    }

    public override void Effect()
    {
        base.Effect();

        startEffect = true;
        levelScore.multiplierScore.text = "x" + levelScore.multiplierState;
        if(silverBullet != null)
        {
            silverBullet.multiplierIsOnSilverBullet = true;
        }
    }

    public override void AfterTimerEffect()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Ennemy");
        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].GetComponent<EnemyMissile>().multiplierIsOnEnemyMissile = false;
        }
        if(silverBullet != null)
        {
            silverBullet.multiplierIsOnSilverBullet = false;
        }
        startEffect = false;
        levelScore.multiplierScore.text = "";
        levelScore.multiplierState++;

        base.AfterTimerEffect();
    }
    //..Corentin SABIAUX GCC2
}
