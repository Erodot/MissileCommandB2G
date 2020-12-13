using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;


public class GameOverAndCie : MonoBehaviour //Coline Marchal
{
    public GameObject preGameOver;
    public GameObject gameOverObject;
    public GameObject victoryObject;
    public GameManager gameManager;

    //SABIAUX Corentin GCC2
    public LevelScoreTest levelScoreTest;
    public HighscoreTableTest highscoreTableTest;

    public bool scoreAdd;
    //..SABIAUX Corentin GCC2

    public Text score;
    public Text combo;
    public Text wave;
    int myScore;
    int myWave;

    public GameObject firstButton;
    public float timeBetweenGameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        //SABIAUX Corentin GCC2
        levelScoreTest = GameObject.Find("LevelScore").GetComponent<LevelScoreTest>();
        highscoreTableTest = GameObject.Find("HighscoreTable").GetComponent<HighscoreTableTest>();
        highscoreTableTest.gameObject.SetActive(false);
        //..SABIAUX Corentin GCC2
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Ennemy");
        foreach (GameObject go in enemys)
        {
            Destroy(go);
        }
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject go in bullets)
        {
            Destroy(go);
        }
        gameManager.turretCanShoot = false;

        //SABIAUX Corentin GCC2
        if (scoreAdd == false)
        {
            highscoreTableTest.gameObject.SetActive(true);
            highscoreTableTest.AddHighscoreEntry(levelScoreTest.gameScore, "Player"); //Add high score entry at AddHigh array | "Player" need to be modified by the name choosen by the player.
            scoreAdd = true;
        }
        //..SABIAUX Corentin GCC2

        StartCoroutine(LoadGameOver());
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton);
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

    IEnumerator LoadGameOver()
    {
        preGameOver.SetActive(true);

        yield return new WaitForSeconds(timeBetweenGameOverScreen);

        preGameOver.SetActive(false);
        gameOverObject.SetActive(true);
        Time.timeScale = 0;
    }
}
