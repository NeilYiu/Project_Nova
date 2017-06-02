using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ProtectionBuff : MonoBehaviour {
    public float speed = 2;
    public int life = 3;
    public bool isPlayerAlive = true;
    public float currentLife = 0;
    void Start()
    {
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
                GameObject.Find("BuffManager/BuffPos3").GetComponent<BuffPos>().activeBuffs.Remove(gameObject);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<Boy>().isInvincible = true;
            other.gameObject.GetComponent<Boy>().invincibleTimer = 10f;

            if (GameObject.Find("EnemyManager/SpawnPos4")!=null)
            {
                List<GameObject> activeBats =
                GameObject.Find("EnemyManager/SpawnPos4").gameObject.GetComponent<SpawnPos>().activeEnemies;
                foreach (GameObject bat in activeBats)
                {
                    Physics2D.IgnoreCollision(bat.gameObject.GetComponent<BoxCollider2D>(), other.gameObject.GetComponent<BoxCollider2D>(), true);
                }
                //GameObject.Find("Canvas/BuffType").GetComponent<Text>().text = "Invincible:";
                GameObject.Find("Canvas/BuffTime").GetComponent<Text>().text = "10";
            }
           

            GameObject.Find("BuffManager/BuffPos3").GetComponent<BuffPos>().activeBuffs.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
