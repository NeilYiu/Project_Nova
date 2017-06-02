using UnityEngine;
using System.Collections;

public class Megalith : MonoBehaviour {

    public float speed = 2;
    public float life = 3;
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
        //if (transform.position.y<=-17f)
        //{
        //    GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        //}
        if (isPlayerAlive)
        {

            transform.Translate(Vector2.left * -GameObject.Find("SceneL").GetComponent<Scroll>().scrollSpeed);

            if (currentLife > 0)
            {
                currentLife -= Time.deltaTime;
            }
            else
            {
                currentLife = life;
                GameObject.Find("EnemyManager/SpawnPos3").GetComponent<SpawnPos>().activeEnemies.Remove(gameObject);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameObject.Find("GameManager").GetComponent<ForestManager>().isPlayerAlive = false;
            Destroy(other.gameObject);
        }
    }
}
