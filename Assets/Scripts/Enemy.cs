using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public int health;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (health <= 0)
	    {
            Instantiate(Resources.Load("Prefabs/MobDie"), transform.position, transform.rotation);
            Destroy(gameObject);
	    }
	}

    public void Damage(int damage)
    {
        health -= damage;
    }
}
