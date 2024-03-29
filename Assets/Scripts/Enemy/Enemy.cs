﻿using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

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
    public Image healthBarUI;
    public float meleePushDistance;
    public float meleePushCountsOfUnitDistance=3;
    public int coins = 10;
    public Text coinText;
    public float dodgeHeight;
    public float dodgeSpeed = 10f;
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
        //Debug.Log(GetComponent<Renderer>().bounds.size.y);
	    dodgeSpeed = (float) (GetComponent<Renderer>().bounds.size.y * 0.2);
        dodgeHeight = GetComponent<Renderer>().bounds.size.y;
	    meleePushDistance = GetComponent<Renderer>().bounds.size.x* meleePushCountsOfUnitDistance;
        coinText = GameObject.Find("PlayerStats/CoinsNum").GetComponent<Text>();
        healthBarUI = transform.Find("EnemyStats/HealthBarBG/Health").GetComponent<Image>();
        ChangeState(new IdleState());
	}

    // Update is called once per frame
    void Update ()
    {
        //0 is left
        healthBarUI.fillOrigin = isFacingRight ? 0 : 1;
        //float distance = Vector3.Distance(target.transform.position, transform.position);
        if (!isDying&&!isTakingDamage)
        {
            //Debug.Log(currentState);
            currentState.Execute();
            LookAtTarget();
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (isDodging)
        {
            //if (dodgeHeight >= 0)
            //{
            //    dodgeHeight -= dodgeSpeed;
            //    transform.Translate(Vector2.up*dodgeSpeed*Time.deltaTime);
            //}
            //else
            //{
            //    dodgeHeight = GetComponent<Renderer>().bounds.size.y*100;
            //    isDodging = false;
            //}
            isDodging = false;

            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
        }
    }

    public void Explode()
    {

        int coinNum = int.Parse(coinText.text);
        coinNum += coins;
        coinText.text = coinNum.ToString();
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
                float distance = Vector3.Distance(other.transform.position, transform.position);
                if (distance < distanceForShotgunToStartDamaging)
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
                    healthBarUI.fillAmount = currentHealth / maxHealth;
                    GetComponent<Animator>().SetTrigger("hit");
                    if (currentHealth <= 0)
                    {
                        isDying = true;

                        GetComponent<Animator>().SetTrigger("die");
                    }
                }
            }
        }
    }
    
    void OnParticleCollision(GameObject other)
    {
        float distance = Vector3.Distance(other.transform.position, transform.position);
        if (other.tag == "ShotgunBullet" && !isDying && distance < distanceForShotgunToStartDamaging)
        {
            currentHealth -= other.GetComponent<ShotgunBullet>().damage;
            healthBarUI.fillAmount = currentHealth / maxHealth;
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
