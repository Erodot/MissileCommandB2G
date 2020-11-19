using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingLifeDamage : MonoBehaviour
{
    //Coline Marchal
    GameManager gameManager;
    public int lifes;
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

    public void Damaged() //damage if neighour attacked
    {
        lifes--;
        //Debug.Log("ouch");
        if (gameManager != null)
        {
            Color damagedColor = new Color();

            if (gameManager.CitiesList.Contains(this.gameObject))//city
            {
                damagedColor = new Color(0.94f, 0.48f, 0.06f);
            }

            else if (gameManager.TurretList.Contains(this.gameObject))//turret
            {
                damagedColor = new Color(1f, 0.19f, 0f);
            }


            GetComponent<Renderer>().material.color = damagedColor;
        }
    }

    public void DestroyThis()
    {
        //Debug.Log("destroy " + name);
        GameObject go = this.gameObject;
        if (gameManager != null)
        {
            if (gameManager.CitiesList.Contains(go))//city
            {
                gameManager.CitiesList.Remove(go);
            }
            else if (gameManager.TurretList.Contains(go))//turret
            {
                gameManager.TurretList.Remove(go);
            }
        }

        Destroy(this.gameObject);
    }
}
