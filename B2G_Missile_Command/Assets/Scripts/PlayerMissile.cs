using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMissile : MonoBehaviour
{
    public GameObject Explosion;
    public Vector3 direction;
    public float speed;
    public Vector3 mousePosition;
    Camera mainCamera;
    public Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -mainCamera.transform.position.z));
    }

    void FixedUpdate()
    {
        Move();
    }
        

    private void Move()
    {
        transform.position += direction.normalized * speed * Time.fixedDeltaTime;

        float distance = Vector3.Distance(startPosition, mousePosition);
        float distance2 = Vector3.Distance(transform.position, startPosition);
        //When mouse position is reached, Instantiate Explosion + self Destruct
        if (distance < distance2)
        {
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Instantiate(Explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


}

