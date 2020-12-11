using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelScoreTest : MonoBehaviour
{
    #region Singleton
    public static LevelScoreTest instance;

    void Awake()
    {

        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(this.gameObject);

            //Rest of your Awake code

        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    public Text levelScore;
    public Text addScore;
    public Text multiplierScore;

    public int previousLevelScore;
    public int stockLevelScore;

    public float timeAddScore;

    // Start is called before the first frame update
    void Start()
    {
        previousLevelScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        levelScore.text = stockLevelScore + "";
    }

    public void AddScore(int score)
    {
        stockLevelScore += score;
        addScore.text = "+ " + score;
        StartCoroutine(scoreToAdd());
    }

    public IEnumerator scoreToAdd()
    {
        addScore.enabled = true;
        yield return new WaitForSeconds(timeAddScore);
        addScore.enabled = false;
    }
}
