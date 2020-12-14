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
        silverBullet = GameObject.Find("SilverBullet").GetComponent<SilverBullet>();
        levelScore = GameObject.Find("LevelScore").GetComponent<LevelScoreTest>();
        Effect();
    }

    // Update is called once per frame
    public override void Update()
    {
        startEffect = true;

        base.Update();
    }

    public override void Effect()
    {
        base.Effect();

        if (startEffect == true)
        {
            GameObject[] enemys = GameObject.FindGameObjectsWithTag("Ennemy");
            for (int i = 0; i < enemys.Length; i++)
            {
                if (enemys[i].GetComponent<EnemyMissile>().lastOfWave)
                {
                    enemys[i].GetComponent<EnemyMissile>().multiplierIsOnEnemyMissile = true;
                }
            }
            silverBullet.multiplierIsOnSilverBullet = true;
        } else
        {
            GameObject[] enemys = GameObject.FindGameObjectsWithTag("Ennemy");
            for (int i = 0; i < enemys.Length; i++)
            {
                if (enemys[i].GetComponent<EnemyMissile>().lastOfWave)
                {
                    enemys[i].GetComponent<EnemyMissile>().multiplierIsOnEnemyMissile = false;
                }
            }
            silverBullet.multiplierIsOnSilverBullet = false;
        }
    }

    public override void AfterTimerEffect()
    {
        startEffect = false;
        levelScore.multiplierState++;

        base.AfterTimerEffect();
    }
    //..Corentin SABIAUX GCC2
}
