using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SpawnEnemy : MonoBehaviour {

    public GameObject[] enemyPrefabs;
    public Transform[] spawnPosArray;
    public float startOverTime = 8;  //Spawn after how many seconds
    public float coolDown = 1;
    public List<GameObject> activeEnemies;
    public bool isPlayerAlive = true;
    public bool isStopped = false;
    public bool isCRRunning = true;
    public float distanceToNextSpawn=20;
    public float currentDistance = 0;
    // Use this for initialization
    void Start () {
        StartCoroutine(Spawn());
        isCRRunning = true;
    }

    void Update()
    {
        if (activeEnemies.Count>0)
        {
            currentDistance = Vector2.Distance(spawnPosArray[0].transform.position, activeEnemies.Last().transform.position);
        }
        if (!isStopped && !isPlayerAlive)
        {
            isCRRunning = false;
            StopCoroutine(Spawn());
            isStopped = true;
            foreach (GameObject enemy in activeEnemies)
            {
                enemy.GetComponent<Obstacle>().isPlayerAlive = false;
            }
        }
        if (isPlayerAlive && isCRRunning==false)
        {
            foreach (GameObject enemy in activeEnemies)
            {
                if (enemy.GetComponent<Obstacle>().isPlayerAlive == false)
                {
                    enemy.GetComponent<Obstacle>().isPlayerAlive = true;
                }
            }
            isCRRunning = true;
            isStopped = false;
            StartCoroutine(Spawn());
        }
    }

    IEnumerator Spawn()
    {
        while (isPlayerAlive)
        {
            foreach (GameObject go in enemyPrefabs)
            {
                foreach (Transform t in spawnPosArray)
                {
                    if (activeEnemies.Count==0)
                    {
                        GameObject temp = GameObject.Instantiate(go, t.position, Quaternion.identity) as GameObject;
                        activeEnemies.Add(temp);
                    }
                    if (currentDistance > distanceToNextSpawn)
                    {
                        distanceToNextSpawn = Random.Range(15, 25);
                        GameObject temp = GameObject.Instantiate(go, t.position, Quaternion.identity) as GameObject;
                        activeEnemies.Add(temp);
                    }
                }
                yield return new WaitForSeconds(coolDown);
            }
            yield return new WaitForSeconds(startOverTime);
        }
    }
}
