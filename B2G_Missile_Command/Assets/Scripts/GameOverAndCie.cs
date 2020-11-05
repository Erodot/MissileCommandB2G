using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameOverAndCie : MonoBehaviour //Coline Marchal
{
    public GameObject gameOverObject;
    public GameObject victoryObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }

        //SceneManager.LoadScene(scene);
    }
}
