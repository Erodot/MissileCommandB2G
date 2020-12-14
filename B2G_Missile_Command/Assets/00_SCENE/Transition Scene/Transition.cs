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
    public Animator tranistionPlanet;

    public float transitionTime = 1f;
    //..Nicolas Pupulin

    // Start is called before the first frame update
    void Start()
    {
        
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

    //Nicolas Pupulin

    public void LoadGame()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        transitionRightCloud.SetTrigger("GameStart");
        transitionLeftCloud.SetTrigger("GameStart");
        transitionUI.SetTrigger("GameStart");
        tranistionPlanet.SetTrigger("GameStart");
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
    //..Nicolas Pupulin
}
