using UnityEngine;
using System.Collections;

public class MoveForwardBuff : MonoBehaviour {
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
                GameObject.Find("BuffManager/BuffPos2").GetComponent<BuffPos>().activeBuffs.Remove(gameObject);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.transform.Translate(Vector2.right*20);
            GameObject.Find("BuffManager/BuffPos2").GetComponent<BuffPos>().activeBuffs.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
