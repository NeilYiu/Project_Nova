using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] spawnPosArray;

    public bool isPlayerAlive = true;
    public bool isStopped = false;

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
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
                }
            }
            isStopped = false;
        }
    }
}

