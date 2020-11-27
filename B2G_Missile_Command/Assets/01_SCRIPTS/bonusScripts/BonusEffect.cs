using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusEffect : MonoBehaviour
{
    public float timeEffect;
    bool startTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (startTimer)
        {
            if (timeEffect > 0)
            {
                timeEffect -= Time.deltaTime;
            }
            else
            {
                AfterTimerEffect();
            }
        } 
    }

    public virtual void Effect()
    {
        startTimer = true;
        Debug.Log("activate bonus !!");
    }

    public virtual void AfterTimerEffect()
    {
        startTimer = false;
        Debug.Log("fin");
    }


}
