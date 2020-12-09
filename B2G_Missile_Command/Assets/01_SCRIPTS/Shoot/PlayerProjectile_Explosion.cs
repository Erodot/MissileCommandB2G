using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile_Explosion : MonoBehaviour
{
    //This value define how many times the Explosion(Sphere) will be multiplied
    public float radiusMultiplier;
    public float explosionTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Explosion());
    }

    public IEnumerator Explosion()
    {
        float wait = radiusMultiplier / (explosionTime * 200);
        for (int i = 1; i < radiusMultiplier; i++)
        {
            transform.localScale = transform.localScale * 1.05f;
            yield return new WaitForSeconds(wait);

            //When Explosion(Sphere) reaches its max scale, it self destruct
            if (i >= radiusMultiplier - 1)
            {
                Destroy(gameObject);
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Do something
    }
}
