using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Transition : MonoBehaviour
{

    //Nicolas Pupulin
    public Animator transitionRightCloud;
    public Animator transitionLeftCloud;
    public Animator transitionUI;
    public Animator transitionPlanet;

    public float transitionTime = 1f;
    public float localTime;

    public GameObject DontDestroy;
    //..Nicolas Pupulin

    // Start is called before the first frame update
    void Start()
    {
        DontDestroy = GameObject.Find("DontDestroy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Nicolas Pupulin
    public void PlayButton()
    {
        LoadGame();
    }

    public void MenuButton()
    {
        LoadMenu();
    }

    //Nicolas Pupulin

    public void LoadGame()
    {
        StartCoroutine(LoadLevel(1));
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
        StartCoroutine(LoadLevel(0));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        if (levelIndex == 1)
        {
            transitionRightCloud.SetTrigger("GameStart");
            transitionLeftCloud.SetTrigger("GameStart");
            transitionUI.SetTrigger("GameStart");
            transitionPlanet.SetTrigger("GameStart");
            transitionPlanet.SetTrigger("GameStart");
            yield return new WaitForSeconds(transitionTime);

            DontDestroyOnLoad(DontDestroy);
            SceneManager.LoadScene(levelIndex);
        } else if (levelIndex == 0)
        {
            GameObject.Find("Pause").SetActive(false);
            transitionRightCloud.SetTrigger("GameEnd");
            transitionLeftCloud.SetTrigger("GameEnd");
            yield return new WaitForSeconds(transitionTime);

            //DontDestroyOnLoad(DontDestroy);
            SceneManager.LoadScene(levelIndex);
        }
    }
    //..Nicolas Pupulin
}
