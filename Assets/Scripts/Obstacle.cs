using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour {

    public float currentDistance = 0;
    public float speed = 2;
    public int life = 3;
    public int coins = 10;
    public SpawnEnemy spawnEnemy;
    public bool isPlayerAlive = true;

    // Use this for initialization
    void Start()
    {
        spawnEnemy = GameObject.Find("EnemyManager").GetComponent<SpawnEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerAlive)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (transform.position.x <= -80.0f)
            {
                spawnEnemy.activeEnemies.Remove(gameObject);
                GameObject.Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Player")
        {
            GameObject.Find("GameManager").GetComponent<ForestManager>().isPlayerAlive = false;
            GameObject.Destroy(other.gameObject);
        }
    }
}
