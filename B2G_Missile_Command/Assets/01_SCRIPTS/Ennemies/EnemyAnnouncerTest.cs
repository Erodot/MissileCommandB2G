using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnnouncerTest : MonoBehaviour
{
    public EnemySpawnTest2 spawner;
    public FieldBuilderTest fieldBuilder;
    public GameObject announcer;

    public int toSpawnEnemy;
    public int previousWave;

    public int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        previousWave = spawner.waveNumber;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyAnnouncer();
    }

    public void EnemyAnnouncer()
    {
        if (i < 1 && fieldBuilder.builderIsOver == true)
        {
            previousWave = spawner.waveNumber;
            toSpawnEnemy = spawner.enemyToSpawn;
            StartCoroutine(AnnouncerAnimation());
            i += 1;
        }

        if (previousWave != spawner.waveNumber)
        {
            Refresh();
        }

        Debug.Log(i);
    }

    IEnumerator AnnouncerAnimation()
    {
        yield return new WaitForSeconds(.001f);
        for (int j = 0; j <= toSpawnEnemy; j++)
        {
            if (spawner.whereSpawn == 0) //top
            {
                GameObject go = Instantiate(announcer, spawner.screenPos + (Vector3.down * 2), Quaternion.identity);
                yield return new WaitForSeconds(spawner.timeToSpawn);
                /*for (int i = 0; i < 5; i++)
                {
                    go.GetComponent<MeshRenderer>().enabled = true;
                    yield return new WaitForSeconds(.2f);
                    go.GetComponent<MeshRenderer>().enabled = false;
                    yield return new WaitForSeconds(.2f);
                }*/
                Destroy(go);
            }
            else if (spawner.whereSpawn == 1) //left
            {
                GameObject go = Instantiate(announcer, spawner.screenPos + (Vector3.left * 4), Quaternion.identity);
                yield return new WaitForSeconds(spawner.timeToSpawn);
                /*for (int i = 0; i < 5; i++)
                {
                    go.GetComponent<MeshRenderer>().enabled = true;
                    yield return new WaitForSeconds(.2f);
                    go.GetComponent<MeshRenderer>().enabled = false;
                    yield return new WaitForSeconds(.2f);
                }*/
                Destroy(go);
            }
            else if (spawner.whereSpawn == 2) //bottom
            {
                GameObject go = Instantiate(announcer, spawner.screenPos + (Vector3.up * 2), Quaternion.identity);
                yield return new WaitForSeconds(spawner.timeToSpawn);
                /*for (int i = 0; i < 5; i++)
                {
                    go.GetComponent<MeshRenderer>().enabled = true;
                    yield return new WaitForSeconds(.2f);
                    go.GetComponent<MeshRenderer>().enabled = false;
                    yield return new WaitForSeconds(.2f);
                }*/
                Destroy(go);
            }
            else if (spawner.whereSpawn == 3) //right
            {
                GameObject go = Instantiate(announcer, spawner.screenPos + (Vector3.right * 4), Quaternion.identity);
                yield return new WaitForSeconds(spawner.timeToSpawn);
                /*for (int i = 0; i < 5; i++)
                {
                    go.GetComponent<MeshRenderer>().enabled = true;
                    yield return new WaitForSeconds(.2f);
                    go.GetComponent<MeshRenderer>().enabled = false;
                    yield return new WaitForSeconds(.2f);
                }*/
                Destroy(go);
            }
        }
    }

    void Refresh()
    {
        i = 0;
    }
}