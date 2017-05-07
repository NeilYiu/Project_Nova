using UnityEngine;
using System.Collections;

public class ShotgunBullet : MonoBehaviour {
    public float bulletSpeed;
    private Rigidbody2D rigidBody2D;
    // Use this for initialization
    void Awake () {
        if (transform.localRotation.z > 0)
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
