using UnityEngine;
using System.Collections;

public class Enemy : Character
{
    public IEnemyState currentState;
    public GameObject target;
    public float meleeRange=1;
    public float shootRange = 10;
    public float distanceForShotgunToStartDamaging=1;
    public float meleeDamage = 2.5f;
    public bool InMeleeRange
    {
        get
        {
            if (target!=null)
            {
                return Vector2.Distance(transform.position, target.transform.position)<=meleeRange;
            }
            return false;
        }
    }
    public bool InShootRange
    {
        get
        {
            if (target != null)
            {
                return Vector2.Distance(transform.position, target.transform.position) <= shootRange;
            }
            return false;
        }
    }
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
        
        if (!isDying&&!isTakingDamage)
        {
            currentState.Execute();
            LookAtTarget();
        }
    }
    
    public void Explode()
    {
        Instantiate(Resources.Load("Prefabs/MobDie"), transform.position, transform.rotation);
        //gameObject.SetActive(false);
        //Destroy(gameObject);
    }
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.tag == "Edge")
        {
            currentState.OnTriggerEnter(other);
        }
        if (damageSource.Contains(other.tag) && !isDying && !isTakingDamage)
        {
            if (other.tag == "Bullet")
            {
                currentHealth -= other.GetComponent<MachineGunBullet>().damage;
            }
            if (other.tag == "PlayerMelee")
            {
                Instantiate(Resources.Load("Prefabs/EnemyHittedByMelee"), transform.position, transform.rotation);
                currentHealth -= other.GetComponent<PlayerSword>().damage;
            }
            GetComponent<Animator>().SetTrigger("hit");
            if (currentHealth <= 0)
            {
                isDying = true;
                GetComponent<Animator>().SetTrigger("die");
            }
        }
    }

    void OnParticleCollision(GameObject other)
    {
        float distance = Vector3.Distance(other.transform.position, transform.position);
        if (other.tag == "ShotgunBullet" && !isDying )
        {
            currentHealth -= other.GetComponent<ShotgunBullet>().damage;
            GetComponent<Animator>().SetTrigger("hit");
            if (currentHealth <= 0)
            {
                isDying = true;
                GetComponent<Animator>().SetTrigger("die");
            }
        }
    }
}
