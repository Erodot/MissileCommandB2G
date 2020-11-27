using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear : BonusEffect
{

    GameObject[] enemysArray;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void Effect()
    {
        enemysArray = GameObject.FindGameObjectsWithTag("Ennemy");
        for (int i = 0; i < enemysArray.Length; i++)
        {
            Destroy(enemysArray[i]);
        }

        base.Effect();
    }
}
