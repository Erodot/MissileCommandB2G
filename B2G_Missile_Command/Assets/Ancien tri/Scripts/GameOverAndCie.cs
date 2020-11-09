using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameOverAndCie : MonoBehaviour //Coline Marchal
{
    public GameObject gameOverObject;
    public GameObject victoryObject;
    public Text score;
    public Text combo;
    public Text wave;
    int myScore;
    int myWave;
    // Start is called before the first frame update
    void Start()
    {
        SetWave();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetScore(int nbScore)
    {
        myScore += nbScore;
        score.text = "\r\n" + myScore;
    }
    public void SetWave()//int nbScore)
    {
        //myWave += nbScore;

        if (LevelsManager.instance != null)
        {
            LevelsManager lvlManager = LevelsManager.instance;
            lvlManager.currentLevel += 1;
            myWave = lvlManager.currentLevel;
        }
            

        wave.text = "\r\n~" + myWave;
    }

    public void GameOver()
    {
        gameOverObject.SetActive(true);
    }
    public void Victory()
    {
        victoryObject.SetActive(true);
    }
    public void LoadScene(string scene)
    {
        if(scene == "replay")
        {
            scene = SceneManager.GetActiveScene().name;

            SceneManager.LoadScene(scene);
        }
        else if (scene == "next")
        {
            //prepare next scene
            scene = SceneManager.GetActiveScene().name;//change if we change levels number

            SceneManager.LoadScene(scene);
        }

        //SceneManager.LoadScene(scene);
    }
}
