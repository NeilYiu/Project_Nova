using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

public class SpawnPos : MonoBehaviour {
    public GameObject[] enemyPrefabs;
    public float coolDownTimer = 0;
    public float maxCoolDown = 9;
    public float minCoolDown = 4;
    public List<GameObject> activeEnemies = new List<GameObject>();
    public bool isPlayerAlive = true;
    public bool isStopped = false;
    private SpawnEnemy spawnEnemy;
    // Use this for initialization
    void Start () {
        coolDownTimer = Random.Range(minCoolDown, maxCoolDown);
        spawnEnemy = GameObject.Find("EnemyManager").GetComponent<SpawnEnemy>();
    }
	
	// Update is called once per frame
	void Update ()
	{
        if (!isStopped && !isPlayerAlive)
        {
            isStopped = true;
            foreach (GameObject enemy in activeEnemies)
            {
                if (enemy.GetComponent<Obstacle>())
                    enemy.GetComponent<Obstacle>().isPlayerAlive = false;

                if (enemy.GetComponent<Arrow>())
                    enemy.GetComponent<Arrow>().isPlayerAlive = false;
            }
        }

        if (coolDownTimer > 0)
	    {
	        coolDownTimer -= Time.deltaTime;
	    }
	    else
	    {
	        coolDownTimer = Random.Range(minCoolDown, maxCoolDown);
            Spawn();
        }
        if (isPlayerAlive)
        {
            foreach (GameObject enemy in activeEnemies)
            {
                if (enemy.gameObject.name == "Obstacle")
                {
                    if (enemy.GetComponent<Obstacle>().isPlayerAlive == false)
                    {
                        enemy.GetComponent<Obstacle>().isPlayerAlive = true;
                    }
                }
                if (enemy.gameObject.name == "Arrow")
                {
                    if (enemy.GetComponent<Arrow>().isPlayerAlive == false)
                    {
                        enemy.GetComponent<Arrow>().isPlayerAlive = true;
                    }
                }
            }
            isStopped = false;
        }
        
    }

    void Spawn()
    {
        if (isPlayerAlive)
        {
            foreach (GameObject enemy in enemyPrefabs)
            {
                //if (!spawnEnemy.hasSpawned)
                //{
                    activeEnemies.Add(GameObject.Instantiate(enemy, transform.position, Quaternion.identity) as GameObject);
                    spawnEnemy.hasSpawned = true;
                //}
            }
        }
    }

    //IEnumerator TrySpawn(GameObject enemy)
    //{
    //    if (spawnEnemy.hasSpawned)
    //    {
    //        yield return new WaitForSeconds(0.3f);
    //    }
    //    else
    //    {
    //        StopCoroutine(TrySpawn(null));
    //        activeEnemies.Add(GameObject.Instantiate(enemy, transform.position, Quaternion.identity) as GameObject);
    //        spawnEnemy.hasSpawned = true;
    //    }
    //}
}
