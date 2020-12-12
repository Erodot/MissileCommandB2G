using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerTest : MonoBehaviour
{
    //Romain Pitot
    float time;
    //..Romain Pitot

    //Corentin SABIAUX GCC2
    void Start()
    {
        time = 0;
    }
    //..Corentin SABIAUX GCC2

    void Update()
    {
        //Romain Pitot
        time = (int)Time.time;
        //..Romain Pitot

        //Corentin SABIAUX GCC2
        int minutes = Mathf.FloorToInt(time / 60F); 
        int seconds = Mathf.FloorToInt(time - minutes * 60);
        string niceTime = string.Format("{00} : {1:00}", minutes, seconds);
        //..Corentin SABIAUX GCC2

        //Romain Pitot
        GetComponent<Text>().text = niceTime;
        //..Romain Pitot
    }
}
