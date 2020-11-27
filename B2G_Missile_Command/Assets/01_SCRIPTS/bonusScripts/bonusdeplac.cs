using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonusdeplac : MonoBehaviour
{
    public float moveSpeed;
    public float baseMoveSpeed;
    public float oscillationSpeed;
    public int direction;


    // Start is called before the first frame update
    void Start()
    {
        baseMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        if(direction == 0)
        {
            transform.position += transform.right * Time.deltaTime * moveSpeed + new Vector3(0, Mathf.Sin(Time.time * 3f) * 1, 0) * Time.deltaTime * oscillationSpeed;
        }
        if (direction == 1)
        {
            transform.position -= transform.right * Time.deltaTime * moveSpeed + new Vector3(0, Mathf.Sin(Time.time * 3f) * 1, 0) * Time.deltaTime * oscillationSpeed;
        }
        if (direction == 2)
        {
            transform.position += transform.up * Time.deltaTime * moveSpeed + new Vector3(Mathf.Sin(Time.time * 3f) * 1, 0, 0) * Time.deltaTime * oscillationSpeed;
        }
        if (direction == 3)
        {
            transform.position -= transform.up * Time.deltaTime * moveSpeed + new Vector3(Mathf.Sin(Time.time * 3f) * 1, 0, 0) * Time.deltaTime * oscillationSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Explosion")) //if the bonus hit a player bullet
        {
            Destroy(gameObject);
        } 
    }
}
