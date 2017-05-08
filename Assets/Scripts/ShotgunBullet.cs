using UnityEngine;
using System.Collections;

public class ShotgunBullet : Bullet {
    public float bulletSpeed;
    public float damage;
    public GameObject explosion;
    // Use this for initialization
    void Awake () {
        //transform.eulerAngles = new Vector3(0,90,0);
        Debug.Log(transform.localRotation.z);
        if (transform.localRotation.x > 0)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 0) * bulletSpeed, ForceMode2D.Impulse);
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0) * bulletSpeed, ForceMode2D.Impulse);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shootable"))
        {
            //decelerate();
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shootable"))
        {
            //decelerate();
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public void decelerate()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
    }
}
