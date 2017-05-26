using UnityEngine;
using System.Collections;

public class AerialMovementBuff : MonoBehaviour {
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
                GameObject.Find("BuffManager/BuffPos1").GetComponent<BuffPos>().activeBuffs.Remove(gameObject);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Boy>().aerialMoveTimer = 10f;
            GameObject.Find("BuffManager/BuffPos1").GetComponent<BuffPos>().activeBuffs.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
