using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTableTest : MonoBehaviour
{
    //A port of Highscore table tutorial from Code Monkey by Corentin SABIAUX GCC2.

    #region Transform references
    private Transform entryContainer; //Reference to transform component of entryContainer, into the scene he had all entries.
    private Transform entryTemplate; //Reference to transform component of template entry.
    private List<Transform> highscoreentryTransformList; //List of all transform component of new entries.
    #endregion

    #region Highscore Table Manager
    [Header("Highscore Table Manager")]
    [Tooltip("Set the distance between two highscore entries")]
    public float templateHeight;
    [Tooltip("Delete all PlayerPrefs, activate it into Unity inspector and run the scene.")]
    public bool erasePlayerPref;
    bool showHighscoreTableTest;
    #endregion

    private void Awake()
    {
        //Debug feature, activate it into Unity inspector and run the scene for delete all PlayerPrefs.
        if (erasePlayerPref == true)
        {
            PlayerPrefs.DeleteAll();
        }

        entryContainer = transform.Find("HighScoreEntryContainer"); //We get the transform of HighScoreEntryContainer gameObject.
        entryTemplate = entryContainer.transform.Find("HighscoreEntryTemplate"); //We get the transform of HighscoreEntryTemplate gameObject.

        entryTemplate.gameObject.SetActive(false); //Don't show entryTemplate.
    }

    public void AddHighscoreEntry(int score, string name)
    {
        //Create HighscoreEntry.
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

        //Load saved Highscores.
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null)
        {
            //If there's no stored table, initialize one.
            highscores = new Highscores()
            {
                highscoreEntryList = new List<HighscoreEntry>()
            };
        }

        //Add new entry to Highscores.
        highscores.highscoreEntryList.Add(highscoreEntry);
        
        //Save updated Highscores.
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();

        RefreshList();
    }

    void RefreshList()
    {
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores != null)
        {
            //Sort entry list by Score
            for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
            {
                for (int j = 0; j < highscores.highscoreEntryList.Count; j++)
                {
                    if (highscores.highscoreEntryList[j].score < highscores.highscoreEntryList[i].score)
                    {
                        //Swap the different entries.
                        HighscoreEntry tmp = highscores.highscoreEntryList[i];
                        highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                        highscores.highscoreEntryList[j] = tmp;
                    }
                }
            }

            highscoreentryTransformList = new List<Transform>();
            foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
            {
                CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreentryTransformList); //Generate the entry into the UI.
            }

            //We don't want to have the highscoreTable larger than 10 entries.
            if (highscores.highscoreEntryList.Count > 10)
            {
                for (int i = 10; i < highscores.highscoreEntryList.Count; i++)
                {
                    Destroy(highscoreentryTransformList[i].gameObject); //Destroy the extra entries.
                }
            }
        }
    }

    public void showHighscoreTableTestInMenu()
    {
        if (!showHighscoreTableTest)
        {
            RefreshList();
            showHighscoreTableTest = true;
        }
    }

    public void changeBoolProperties()
    {
        showHighscoreTableTest = false;
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        Transform entryTransform = Instantiate(entryTemplate, container); //Set entryTransform based on entryTemplate transform and set into entryContainer.
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count); //Set the distance between two entries.
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default: rankString = rank + " -"; break; //Set rank text like : "1 -" / "2 -" ...
        }

        entryTransform.Find("PosText").GetComponent<Text>().text = rankString;

        string name = highscoreEntry.name; //Set the username choosed by the player into the highscore entry.

        entryTransform.Find("NameText").GetComponent<Text>().text = name;


        int score = highscoreEntry.score; //Set the gameScore from the round into the highscore entry.

        entryTransform.Find("ScoreText").GetComponent<Text>().text = score.ToString("000 000"); //Convert the integer score value into string with the format "000 000".

        transformList.Add(entryTransform); //Add the transform of this new highscore entry into the list.
    }

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }


    [System.Serializable]
    private class HighscoreEntry {
        public int score;
        public string name;
    }
    //..Corentin SABIAUX GCC2
}
