using UnityEngine;
using System.Collections;

public class ShotgunBuff : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().isUsingShotgun = true;
            other.GetComponent<Player>().bullet = (GameObject) Resources.Load("Prefabs/Bullets/ShotgunBullet", typeof(GameObject));
            other.GetComponent<Player>().coolDown =
                other.GetComponent<Player>().bullet.GetComponent<ShotgunBullet>().coolDown;
            Destroy(gameObject);
        }
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().health -= 10;
        }
    }
}
