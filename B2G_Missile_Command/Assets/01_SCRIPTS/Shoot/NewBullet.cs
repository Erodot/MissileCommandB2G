using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBullet : MonoBehaviour
{

    public GameObject Explosion;
    public int speed;
    public int lifeTime;
    public Vector3 direction;
    public float explosionRadius;
    public bool canExplode;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Life());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction.normalized * Time.deltaTime * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Explosion") || other.gameObject.CompareTag("Bullet"))
        {

        }
        else
        {
            Explose();
        }
    }

    private void Explose()
    {
        if (canExplode)
        {
            GameObject go = Instantiate(Explosion, transform.position, Quaternion.identity);
            go.GetComponent<PlayerProjectile_Explosion>().radiusMultiplier = explosionRadius;
        }
        Destroy(gameObject);
    }

    IEnumerator Life()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);

    }
}
