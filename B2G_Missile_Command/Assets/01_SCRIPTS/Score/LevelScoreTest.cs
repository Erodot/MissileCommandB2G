using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelScoreTest : MonoBehaviour
{
    //Script by Corentin SABIAUX GCC2, don't hesitate to ask some questions.
 

    #region Text Storage
    [Header("Text Storage")]
    public Text levelScore;
    public Text addScore;
    public Text multiplierScore;
    #endregion

    #region Score Manager
    [Header("Score Manager")]
    public int gameScore;
    public float timeToShowAddedScore;
    public float timeToShowMultiplierScore;
    public int respawnPallier;
    int pallier;
    #endregion

    void Update()
    {
        levelScore.text = gameScore.ToString("000 000") + "";  //Show gameScore value on screen.
    }

    public void AddScore(int score)
    {
        CheckScore(gameScore, gameScore + score);
        gameScore += score; //Add enemy score previously killed.
        addScore.text = "+ " + score; //Show the value added on screen.
        StartCoroutine(showScoreToAdd());
    }

    public void MultiplierScore(int score, int multiplier)
    {
        gameScore += score * multiplier;
        addScore.text = "+ " + score * multiplier;
        StartCoroutine(showScoreToAdd());
    }

    public IEnumerator showScoreToAdd()
    {
        addScore.enabled = true; //The text AddScore is show at screen ...
        yield return new WaitForSeconds(timeToShowAddedScore); //.. during the amount of time set at timeToShowAddedScore.
        addScore.enabled = false; //When it's over, the text disappears.
    }
    //..Corentin SABIAUX GCC2

    //Julien MACHADO GCC2
    void CheckScore(int score, int scoreToBe)
    {
        if(score < respawnPallier * (pallier + 1) && scoreToBe >= respawnPallier * (pallier + 1))
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().ReviveTurret();
            pallier++;
        }
    }
    //..Julien MACHADO GCC2
}
