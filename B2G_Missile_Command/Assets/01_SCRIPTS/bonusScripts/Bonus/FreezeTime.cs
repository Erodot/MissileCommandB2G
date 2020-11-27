using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTime : BonusEffect
{

    GameObject[] enemysArray;
    GameObject[] bonusArray;
    public float speedDivider;

    bool startEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (startEffect)
        {
            enemysArray = GameObject.FindGameObjectsWithTag("Ennemy");
            bonusArray = GameObject.FindGameObjectsWithTag("Bonus");

            foreach (GameObject enemy in enemysArray)
            {
                enemy.GetComponent<EnemyMissile>().speed = enemy.GetComponent<EnemyMissile>().baseSpeed / speedDivider;
            }
            foreach (GameObject bonus in bonusArray)
            {
                bonus.GetComponent<bonusdeplac>().moveSpeed = bonus.GetComponent<bonusdeplac>().baseMoveSpeed / speedDivider;
            }
        }
    }

    public override void Effect()
    {
        startEffect = true;

        base.Effect();
    }

    public override void AfterTimerEffect()
    {
        enemysArray = GameObject.FindGameObjectsWithTag("Ennemy");
        bonusArray = GameObject.FindGameObjectsWithTag("Bonus");

        foreach (GameObject enemy in enemysArray)
        {
            enemy.GetComponent<EnemyMissile>().speed = enemy.GetComponent<EnemyMissile>().baseSpeed;
        }
        foreach (GameObject bonus in bonusArray)
        {
            bonus.GetComponent<bonusdeplac>().moveSpeed = bonus.GetComponent<bonusdeplac>().baseMoveSpeed;
        }

        startEffect = false;

        base.AfterTimerEffect();
    }
}
