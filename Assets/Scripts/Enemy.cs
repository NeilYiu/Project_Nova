using UnityEngine;
using System.Collections;

public class Enemy : Character
{
    private IEnemyState currentState;
    public int health;

    public void Move()
    {
        GetComponent<Animator>().SetBool("isWalking",true);
        if (isFacingRight)
        {
            transform.Translate(Vector2.right*speed*Time.deltaTime);
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
