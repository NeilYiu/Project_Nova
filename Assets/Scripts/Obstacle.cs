using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour {

    //public float currentDistance = 0;
    public float speed = 2;
    public int life = 3;
    public int coins = 10;
    public SpawnEnemy spawnEnemy;
    public bool isPlayerAlive = true;
    public float currentLife = 0;
    // Use this for initialization
    void Start()
    {
        spawnEnemy = GameObject.Find("EnemyManager").GetComponent<SpawnEnemy>();
        currentLife = life;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerAlive)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            
            if (currentLife > 0)
            {
                currentLife -= Time.deltaTime;
            }
            else
            {
                currentLife = life;
                GameObject.Find("EnemyManager/SpawnPos1").GetComponent<SpawnPos>().activeEnemies.Remove(gameObject);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Player" && !other.gameObject.GetComponent<Boy>().isInvincible)
        {
            GameObject.Find("GameManager").GetComponent<ForestManager>().isPlayerAlive = false;
            GameObject.Destroy(other.gameObject);
        }
    }
}
