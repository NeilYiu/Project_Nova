using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {
    public float coolDown=2;
    private float coolDownTimer;
    public float pushBackForce=30f;
    public float damage=0.1f;
    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (coolDownTimer > 0)
        {
            coolDownTimer -= Time.deltaTime;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && coolDownTimer<=0)
        {
            coolDownTimer = coolDown;
            other.GetComponent<Player>().currentHealth -= damage;
            push(other.transform);
        }
    }

    void push(Transform player)
    {
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //player.GetComponent<Rigidbody2D>().AddForce(new Vector2(player.position.x - transform.position.x,
        //    player.position.y-transform.position.y).normalized * pushBackForce,ForceMode2D.Impulse);
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0,
            player.position.y - transform.position.y).normalized * pushBackForce;

    }
}
