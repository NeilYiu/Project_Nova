using UnityEngine;
using System.Collections;

public class Enemy : Character
{
    private IEnemyState currentState;
    public int health;
    public GameObject target;
    public bool isOnEdge;
    public void Move()
    {
        GetComponent<Animator>().SetBool("isWalking",true);
        if (isFacingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }
    
    private void LookAtTarget()
    {
        if (target != null)
        {
            float lookDir = target.transform.position.x - transform.position.x;
            if (lookDir<0 && isFacingRight || lookDir>0 && !isFacingRight)
            {
                ChangeDirection();
            }
        }
    }
    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter(this);
    }

	// Use this for initialization
	public override void Start () {
	    base.Start();
        ChangeState(new IdleState());
	}

    // Update is called once per frame
    void Update () {
        currentState.Execute();
        Debug.Log(currentState.GetType().ToString());
	    if (health <= 0)
	    {
            Instantiate(Resources.Load("Prefabs/MobDie"), transform.position, transform.rotation);
            Destroy(gameObject);
	    }
        LookAtTarget();
	}

    public void Damage(int damage)
    {
        health -= damage;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Edge")
        {
            //isOnEdge = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Edge")
        {
            //isOnEdge = true;
            currentState.OnTriggerEnter(other);
        }
        
    }
}
