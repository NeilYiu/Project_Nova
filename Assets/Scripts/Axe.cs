using UnityEngine;
using System.Collections;

public class Axe : MonoBehaviour {
    public float speed=10f;
    public float rotationSpeed=1f;
    public GameObject player;
    public Vector2 vertex = new Vector2(20.0f,20.0f);
	// Use this for initialization
	void Start () {
	    player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
	{
	    //float deltaX = speed * Time.deltaTime;
	    //float deltaY = Mathf.Pow(deltaX - player.transform.position.x - vertex.x,2);
        transform.Translate(Vector2.right*speed*Time.deltaTime,Space.World);
        transform.Rotate(new Vector3(0,0,rotationSpeed));
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bat")
        {
            GameObject.Find("EnemyManager/SpawnPos4").GetComponent<SpawnPos>().activeEnemies.Remove(other.gameObject);
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Arrow")
        {
            GameObject.Find("EnemyManager/SpawnPos2").GetComponent<SpawnPos>().activeEnemies.Remove(other.gameObject);
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Obstacle")
        {
            GameObject.Find("EnemyManager/SpawnPos1").GetComponent<SpawnPos>().activeEnemies.Remove(other.gameObject);
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Megalith")
        {
            GameObject.Find("EnemyManager/SpawnPos3").GetComponent<SpawnPos>().activeEnemies.Remove(other.gameObject);
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
