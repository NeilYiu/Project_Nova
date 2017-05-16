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
    public bool isDodging = false;
    public int flee = 50;
    [SerializeField]
    private Transform leftEdge;
    [SerializeField]
    private Transform rightEdge;

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
        if (isAttacking || isDodging)
        {
            return;
        }
        GetComponent<Animator>().SetBool("isWalking",true);
        if (isFacingRight && rightEdge.position.x > transform.position.x)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else if (!isFacingRight && leftEdge.position.x < transform.position.x)
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
    void Update ()
    {
        //float distance = Vector3.Distance(target.transform.position, transform.position);
        if (!isDying&&!isTakingDamage)
        {
            Debug.Log(currentState);
            currentState.Execute();
            LookAtTarget();
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (isDodging)
        {
            isDodging = false;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0,5),ForceMode2D.Impulse);
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
            isDodging = TryDodge();
            if (!isDodging)
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
    }

    //void OnTriggerStay2D(Collider2D other)
    //{
    //    if (other.tag=="Edge")
    //    {
    //        currentState.OnTriggerEnter(other);
    //    }
    //}

    void OnParticleCollision(GameObject other)
    {
        float distance = Vector3.Distance(other.transform.position, transform.position);
        if (other.tag == "ShotgunBullet" && !isDying && distance < distanceForShotgunToStartDamaging)
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

    bool TryDodge()
    {
        int number = UnityEngine.Random.Range(1, 100);
        if (number < flee)
        {
            return true;
        }
        return false;
    }
}
