using UnityEngine;
using System.Collections;

public class ArielSpeedBuff : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Player")
        {
            other.GetComponent<Player>().arielSpeed = 10;
            Destroy(gameObject);
        }
    }

}
