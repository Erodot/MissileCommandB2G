using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusEffect : MonoBehaviour
{
    public float timeEffect;
    bool startTimer;
    public Vector3 effectPos;

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
        GameObject.FindGameObjectWithTag("audio").GetComponent<SoundManager>().Play("bonus");
    }

    public virtual void AfterTimerEffect()
    {
        startTimer = false;
        Debug.Log("fin");
        Destroy(gameObject);
    }


}
