using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnerScript : MonoBehaviour
{
    public GameObject spawnEnemy;
    public float spawnDelay = 1.2f;
    public float nextTimeToSpawn = 0f;
    public Transform[] spawnPoints;
    private int currentSpawnPoint = -1;

    void FixedUpdate()
    {
        if (nextTimeToSpawn <= Time.time)
        {
            SpawnEnemy();
            nextTimeToSpawn = Time.time + spawnDelay;
        }
    }

    private void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        if (randomIndex == currentSpawnPoint)
        {
            while (randomIndex == currentSpawnPoint)
            {
                randomIndex = Random.Range(0, spawnPoints.Length);
            }
        }
        currentSpawnPoint = randomIndex;
        Transform sp = spawnPoints[randomIndex];
        Instantiate(spawnEnemy, sp.position, sp.rotation);
    }
}
