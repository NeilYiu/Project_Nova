using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] spawnPosArray;

    public bool isPlayerAlive = true;
    public bool isStopped = false;
    public float minSpawnInterval = 1f;
    private float minSpawnTimer;

    public bool hasSpawned = false;
    // Use this for initialization
    void Start()
    {
        minSpawnTimer = minSpawnInterval;
    }

    void Update()
    {
        if (hasSpawned)
        {
            if (minSpawnTimer <= 0)
            {
                hasSpawned = false;
                minSpawnTimer = minSpawnInterval;
            }
            minSpawnTimer -= Time.deltaTime;
        }
        if (!isStopped && !isPlayerAlive)
        {
            isStopped = true;
            foreach (GameObject pos in spawnPosArray)
            {
                pos.gameObject.GetComponent<SpawnPos>().isPlayerAlive = false;
                foreach (var enemy in pos.GetComponent<SpawnPos>().activeEnemies)
                {
                    if (pos.gameObject.name == "SpawnPos1")
                        enemy.GetComponent<Obstacle>().isPlayerAlive = false;

                    if (pos.gameObject.name == "SpawnPos2")
                        enemy.GetComponent<Arrow>().isPlayerAlive = false;

                    if (pos.gameObject.name == "SpawnPos3")
                        enemy.GetComponent<Megalith>().isPlayerAlive = false;

                    if (pos.gameObject.name == "SpawnPos4")
                        enemy.GetComponent<Bat>().isPlayerAlive = false;
                }
            }
        }
        if (isPlayerAlive)
        {
            foreach (GameObject pos in spawnPosArray)
            {
                pos.gameObject.GetComponent<SpawnPos>().isPlayerAlive = true;
                foreach (var enemy in pos.GetComponent<SpawnPos>().activeEnemies)
                {
                    if (pos.gameObject.name == "SpawnPos1")
                        enemy.GetComponent<Obstacle>().isPlayerAlive = true;

                    if (pos.gameObject.name == "SpawnPos2")
                        enemy.GetComponent<Arrow>().isPlayerAlive = true;

                    if (pos.gameObject.name == "SpawnPos3")
                        enemy.GetComponent<Megalith>().isPlayerAlive = true;

                    if (pos.gameObject.name == "SpawnPos4")
                        enemy.GetComponent<Bat>().isPlayerAlive = true;
                }
            }
            isStopped = false;
        }
    }
}

