using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnnouncerTest : MonoBehaviour
{
    //Script by Corentin SABIAUX GCC2, don't hesitate to ask some questions.
    //Not finalized, have been paused due to task ambiguity.

    public EnemySpawnTest2 spawner; //Stock the class EnemySpawnTest2. 
    public FieldBuilderTest fieldBuilder; //Stock the class FieldBuilderTest.
    public GameObject announcer; //Stock the announcer Prefab.

    public int toSpawnEnemy;
    public int previousWave;

    public int i = 0;

    void Start()
    {
        previousWave = spawner.waveNumber; //We stock here the first waveNumber into the int previousWave.
    }

    void Update()
    {
        EnemyAnnouncer(); //We called the function EnemyAnnouncer.
    }

    public void EnemyAnnouncer()
    {
        if (i < 1 && fieldBuilder.builderIsOver == true) //When the planet is totally builded and we have to spawn some announcers.
        {
            previousWave = spawner.waveNumber;
            toSpawnEnemy = spawner.enemyToSpawn; //We stock the number of enemies to spawn into an int dedicated. 
            StartCoroutine(AnnouncerAnimation()); //We called the coroutine AnnouncerAnimation.
            i += 1;
        }

        if (previousWave != spawner.waveNumber) //When a new wave is coming.
        {
            Refresh();
        }
    }

    IEnumerator AnnouncerAnimation()
    {
        yield return new WaitForSeconds(.001f); //Security time, let the program get all he need before instantiate.
        for (int j = 0; j <= toSpawnEnemy; j++) //For each enemies to spawn. 
        { //Check where he will spawn into the screen.
            if (spawner.whereSpawn == 0) //Is it top ?
            {
                GameObject go = Instantiate(announcer, spawner.screenPos + (Vector3.down * 2), Quaternion.identity); //Announcer is spawned with a little gap in positions.
                yield return new WaitForSeconds(spawner.timeToSpawn); //When the next enemy have to spawn.
                Destroy(go); //Destroy the previous Announcer.
            }
            else if (spawner.whereSpawn == 1) //Is it left ?
            {
                GameObject go = Instantiate(announcer, spawner.screenPos + (Vector3.left * 4), Quaternion.identity);
                yield return new WaitForSeconds(spawner.timeToSpawn);
                Destroy(go);
            }
            else if (spawner.whereSpawn == 2) //Is it bottom ?
            {
                GameObject go = Instantiate(announcer, spawner.screenPos + (Vector3.up * 2), Quaternion.identity);
                yield return new WaitForSeconds(spawner.timeToSpawn);
                Destroy(go);
            }
            else if (spawner.whereSpawn == 3) //Is it right ?
            {
                GameObject go = Instantiate(announcer, spawner.screenPos + (Vector3.right * 4), Quaternion.identity);
                yield return new WaitForSeconds(spawner.timeToSpawn);
                Destroy(go);
            }
        }
    }

    void Refresh()
    {
        i = 0;
    }

    //..Corentin SABIAUX GCC2
}