using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bat : MonoBehaviour {
    public float speed = 2;
    public float life = 3;
    public int coins = 10;
    public SpawnEnemy spawnEnemy;
    public bool isPlayerAlive = true;
    public float currentLife = 0;
    public float maxHeight = 10f;
    public float minHeight = -8f;
    public bool isMovingUp = false;
    public Boy boy;
    // Use this for initialization
    void Start()
    {
        spawnEnemy = GameObject.Find("EnemyManager").GetComponent<SpawnEnemy>();
        currentLife = life;
        boy = GameObject.FindWithTag("Player").gameObject.GetComponent<Boy>();
        if (GameObject.FindWithTag("Player").gameObject.GetComponent<Boy>().isInvincible)
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), GameObject.FindWithTag("Player").gameObject.GetComponent<BoxCollider2D>(), true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerAlive)
        {
            if (transform.position.y <= minHeight)
            {
                //move up
                isMovingUp = true;
            }
            if (transform.position.y >= maxHeight)
            {
                //move down
                isMovingUp = false;
            }
            if (isMovingUp)
            {
                transform.Translate(new Vector2(-1, 1) * speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(new Vector2(-1, -1) * speed * Time.deltaTime);
            }
            if (currentLife > 0)
            {
                currentLife -= Time.deltaTime;
            }
            else
            {
                currentLife = life;
                GameObject.Find("EnemyManager/SpawnPos4").GetComponent<SpawnPos>().activeEnemies.Remove(gameObject);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.tag == "Player" && !other.gameObject.GetComponent<Boy>().isInvincible)
        //{
        //    GameObject.Find("GameManager").GetComponent<ForestManager>().isPlayerAlive = false;
        //    Destroy(other.gameObject);
        //}
        if (other.tag == "Player" && !other.gameObject.GetComponent<Boy>().isInvincible)
        {
            //GameObject.Find("GameManager").GetComponent<ForestManager>().isPlayerAlive = false;
            other.gameObject.GetComponent<Boy>().currentHealth -= 1;
            if (SceneManager.GetActiveScene().name == "Level4")
                GameObject.Find("Canvas/CurrentHealth").GetComponent<Text>().text = (int.Parse(GameObject.Find("Canvas/CurrentHealth").GetComponent<Text>().text) - 1).ToString();

        }
    }
}
