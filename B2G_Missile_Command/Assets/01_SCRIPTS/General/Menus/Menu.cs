using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public Transition play;
    public SettingsMenu settings;
    public SoundManager audio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        int r = Random.Range(0, 1);
        audio.Play(audio.sounds[1].list[r]);
    }
    public void Play()
    {
        Click();
        play.PlayButton();
    }
    public void ShowScores()
    {
        Click();
    }
    public void ShowCredits()
    {
        Click();
    }
    public void Settings()
    {
        settings.SettingsButton();
    }
    public void Control()
    {
        settings.ControlButton();
    }
    public void Quit()
    {
        audio.Play(audio.sounds[1].list[2]);
        settings.QuitButton();
    }
    public void SetResolution(int r)
    {
        Click();
        settings.SetResolution(r);
    }
    public void SetFullScreen(bool b)
    {
        Click();
        settings.SetFullscreen(b);
    }
    public void SetVolumeSound(float v)
    {
        Click();
        settings.SetVolumeSound(v);
    }
    public void SetVolumeMusic(float v)
    {
        Click();
        settings.SetVolumeMusic(v);
    }
    public void Back()
    {
        audio.Play(audio.sounds[1].list[2]);
        settings.ReturnButton();
    }


}
