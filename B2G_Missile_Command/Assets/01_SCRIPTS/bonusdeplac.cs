using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonusdeplac : MonoBehaviour
{
    public int moveSpeed;
    public int oscillationSpeed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime *moveSpeed + new Vector3(0, Mathf.Sin(Time.time * 3f) * 1,0) * Time.deltaTime * oscillationSpeed ;
    }
}
