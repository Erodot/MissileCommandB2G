using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : MonoBehaviour
{
    public Vector3 direction;
    public float speed;

    void Start()
    {

    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += direction.normalized * speed * Time.deltaTime;
    }
}

