using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingLifeDamage : MonoBehaviour
{
    //Coline Marchal
    GameManager gameManager;
    public int lifes;
    public bool destroyed;
    public string type;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(lifes <= 0)
        {
            DestroyThis();
        }
    }

    public void ManageSound(string what)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioClip sound = null;
        SoundManager sm = GameObject.FindGameObjectWithTag("audio").GetComponent<SoundManager>();
        if(type == "turret")
        {
            if (what == "shoot")
            {
                int r = Random.Range(0, sm.sounds[3].list.Count - 1);
                sound = sm.sounds[3].list[r];
            }
            else if (what == "damage")
            {
                
            }
            else if (what == "destroy")
            {
                Debug.Log("brolhomom");
                sound = sm.sounds[5].list[1];
            }
        }
        else //city
        {
            if (what == "damage")
            {
                
            }
            else if (what == "destroy")
            {
                Debug.Log("brolhomom");
                sound = sm.sounds[5].list[2];
            }
        }

        audioSource.clip = sound;
        audioSource.Play();
        
    }

    public void Damaged(int damagedAmount) //damage if neighour attacked
    {
        lifes -= damagedAmount;
        //Debug.Log("ouch");
        if (gameManager != null)
        {
            /*/Color damagedColor = new Color();

            if (gameManager.CitiesList.Contains(this.gameObject))//city
            {
                damagedColor = new Color(0.94f, 0.48f, 0.06f);
            }

            else if (gameManager.TurretList.Contains(this.gameObject))//turret
            {
                damagedColor = new Color(1f, 0.19f, 0f);
            }*/

            transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
            ManageSound("damage");
            //GetComponent<Renderer>().material.color = damagedColor;
        }
    }

    public void DestroyThis()
    {
        //Debug.Log("destroy " + name);
        GameObject go = this.gameObject;
        //go.GetComponent<MeshRenderer>().enabled = false;
        destroyed = true;

        transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);

        ManageSound("destroy");

        if (gameManager != null)
        {
            if (gameManager.CitiesList.Contains(go))//city
            {
                gameManager.CitiesList.Remove(go);
            }
            else if (gameManager.TurretList.Contains(go))//turret
            {
                gameManager.TurretList.Remove(go);
                go.GetComponent<Shoot>().isDestroy = true;
                gameManager.LastActivated = gameManager.TurretList[0];
                gameManager.LastActivated.GetComponent<Shoot>().isActivated = true;
            }
        }

        //Destroy(this.gameObject);
    }
}
