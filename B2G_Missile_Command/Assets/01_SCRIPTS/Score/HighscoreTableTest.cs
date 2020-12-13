using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTableTest : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreentryTransformList;

    public float templateHeight;
    public bool erasePlayerPref;

    private void Awake()
    {
        if (erasePlayerPref == true)
        {
            PlayerPrefs.DeleteAll();
        }

        entryContainer = transform.Find("HighScoreEntryContainer");
        entryTemplate = entryContainer.transform.Find("HighscoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null)
        {
            AddHighscoreEntry(0, "Mélodie");
            // Reload
            jsonString = PlayerPrefs.GetString("highscoreTable");
            highscores = JsonUtility.FromJson<Highscores>(jsonString);

            //Sort entry list by Score
            for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
            {
                for (int j = 0; j < highscores.highscoreEntryList.Count; j++)
                {
                    if (highscores.highscoreEntryList[j].score < highscores.highscoreEntryList[i].score)
                    {
                        //Swap
                        HighscoreEntry tmp = highscores.highscoreEntryList[i];
                        highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                        highscores.highscoreEntryList[j] = tmp;
                    }
                }
            }

            highscoreentryTransformList = new List<Transform>();
            foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
            {
                CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreentryTransformList);
            }

            if (highscores.highscoreEntryList.Count > 10)
            {
                highscores.highscoreEntryList.RemoveAt(9);
            }
        }
    }

    void RefreshList()
    {
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if(highscores != null)
        {
            //Sort entry list by Score
            for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
            {
                for (int j = 0; j < highscores.highscoreEntryList.Count; j++)
                {
                    if (highscores.highscoreEntryList[j].score < highscores.highscoreEntryList[i].score)
                    {
                        //Swap
                        HighscoreEntry tmp = highscores.highscoreEntryList[i];
                        highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                        highscores.highscoreEntryList[j] = tmp;
                    }
                }
            }

            highscoreentryTransformList = new List<Transform>();
            foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
            {
                CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreentryTransformList);
            }

            if (highscores.highscoreEntryList.Count > 10)
            {
                highscores.highscoreEntryList.RemoveAt(9);
            }
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default: rankString = rank + " -"; break;
        }

        entryTransform.Find("PosText").GetComponent<Text>().text = rankString;

        string name = highscoreEntry.name;

        entryTransform.Find("NameText").GetComponent<Text>().text = name;


        int score = highscoreEntry.score;

        entryTransform.Find("ScoreText").GetComponent<Text>().text = score.ToString("000 000");

        transformList.Add(entryTransform);
    }

    public void AddHighscoreEntry(int score, string name)
    {
        //Create HighscoreEntry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

        //Load saved Highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null)
        {
            // There's no stored table, initialize
            highscores = new Highscores()
            {
                highscoreEntryList = new List<HighscoreEntry>()
            };
        }

        //Add new entry to Highscores
        highscores.highscoreEntryList.Add(highscoreEntry);
        
        //Save updated Highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();

        RefreshList();
    }

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    //Test High score entry
    [System.Serializable]
    private class HighscoreEntry {
        public int score;
        public string name;
    }
}
