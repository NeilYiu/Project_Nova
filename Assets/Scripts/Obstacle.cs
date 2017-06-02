using UnityEngine;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour {

    //public float currentDistance = 0;
    public float speed = 2;
    public int life = 20;
    public int coins = 10;
    public SpawnEnemy spawnEnemy;
    public bool isPlayerAlive = true;
    public float currentLife = 0;
    public float verticalSpeed = 5f;
    public bool isMovingUp = false;
    // Use this for initialization
    void Start()
    {
        spawnEnemy = GameObject.Find("EnemyManager").GetComponent<SpawnEnemy>();
        currentLife = life;
        transform.position = new Vector3(transform.position.x,-15f,0f);
    }

    // Update is called once per frame
    void Update()
    {


        if (transform.position.y >= -19.3f)
        {
            isMovingUp = false;
        }
        if (transform.position.y <= -25f)
        {
            isMovingUp = true;
        }
        if (isPlayerAlive)
        {
            if (!isMovingUp)
            {
                transform.Translate(Vector2.down * verticalSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector2.up * verticalSpeed * Time.deltaTime);
            }
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            //transform.Translate(Vector2.left * -GameObject.Find("SceneL").GetComponent<Scroll>().scrollSpeed);

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
        if (other.tag == "Player" && !other.gameObject.GetComponent<Boy>().isInvincible)
        {
            //GameObject.Find("GameManager").GetComponent<ForestManager>().isPlayerAlive = false;
            other.gameObject.GetComponent<Boy>().currentHealth -= 1;
            GameObject.Find("Canvas/CurrentHealth").GetComponent<Text>().text = (int.Parse(GameObject.Find("Canvas/CurrentHealth").GetComponent<Text>().text) - 1).ToString();
            //Destroy(gameObject);
        }
    }
}
