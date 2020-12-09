using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public string place; //in game or menu

    [Serializable]
    public struct Sounds
    {
        public string type;
        public List<AudioClip> list;
    }
    [SerializeField]
    public Sounds[] sounds;


    // Start is called before the first frame update
    void Start()
    {
        if (place == "IG")
        {
            int random = UnityEngine.Random.Range(0, sounds[2].list.Count - 1);
            PlayMusic(sounds[2].list[random]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayMusic(AudioClip music)
    {
        audioSource.clip = music;
        audioSource.Play();
    }
}
