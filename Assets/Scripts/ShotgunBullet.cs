using UnityEngine;
using System.Collections;

public class ShotgunBullet : Bullet {
    public float bulletSpeed;
    public float damage;
    public float coolDown = 0.7f;

    // Use this for initialization
    void Awake () {
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
}
