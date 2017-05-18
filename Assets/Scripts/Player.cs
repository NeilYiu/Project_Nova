﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : Character {
    public float arielSpeed = 3f;
    public float jumpHeight=6;
    public bool isGrounded;
    public Transform foot;
    public float groundCheckRadius;
    public LayerMask ground;
    public bool isInvincible=false;
    public float invincibleTime = 10f;
    public float invincibleTimer;
    public Image healthBarUI;
    public bool isBeingPushed;
    public float pushedDistance;
    private bool isPushedToRight;
    public float pushedSpeed = 10;
    public float jumpedHeight = 0;
    [SerializeField]
    private float jumpSpeed = 5;
    // Use this for initialization
    public override void Start ()
    {
        base.Start();
        healthBarUI = GameObject.Find("PlayerStats/HealthBarBG/Health").GetComponent<Image>();
        invincibleTimer = invincibleTime;
    }
    public override void FixedUpdate()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
        }
        if (invincibleTimer<=0)
        {
            isInvincible = false;
            invincibleTimer = invincibleTime;
        }
        isGrounded = Physics2D.OverlapCircle(foot.transform.position, groundCheckRadius, ground);
        if (isGrounded)
        {
            jumpedHeight = 0;
        }
        base.FixedUpdate();
        DetectInputs();
        if (isBeingPushed)
        {
            //Debug.Log(pushedDistance);
            if (pushedDistance > 0)
            {
                pushedDistance -= pushedSpeed * Time.deltaTime;
                
                if (isPushedToRight)
                {
                    if (!Input.GetKey(KeyCode.A))
                    {
                        transform.Translate(Vector2.right * pushedSpeed * Time.deltaTime);
                    }
                    else
                    {
                        transform.Translate(Vector2.right * pushedSpeed *0.5f* Time.deltaTime);
                    }
                }
                else
                {
                    if (!Input.GetKey(KeyCode.D))
                    {
                        transform.Translate(Vector2.left * pushedSpeed * Time.deltaTime);
                    }
                    else
                    {
                        transform.Translate(Vector2.left * pushedSpeed * 0.5f* Time.deltaTime);
                    }
                }
            }
            else
            {
                isBeingPushed = false;
            }
        }
    }

    // Update is called once per frame
    public override void Update () {
        //DetectInputs();
        base.Update();
        if (currentHealth<=0)
        {           
            Instantiate(Resources.Load("Prefabs/PlayerDie"),gameObject.transform.position, Quaternion.Euler(new Vector3(270, 0, 0)));
            Destroy(gameObject);
        }
    }
    

    private void DetectInputs()
    {
        if (isAttacking)
        {
            return;
        }
        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Animator>().SetBool("isWalking", true);
            if (isFacingRight)
            {
                //Face to the moving direction
                ChangeDirection();
            }

            if (isGrounded)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-arielSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Animator>().SetBool("isWalking", true);
            if (!isFacingRight)
            {
                //Face to the moving direction
                ChangeDirection();
            }
            if (isGrounded)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(arielSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
        }
        else
        {
            GetComponent<Animator>().SetBool("isWalking", false);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        }

        if (Input.GetKey(KeyCode.W))
        {
            //AddForce BUG: ONLY BEHAVE THE SAME WHEN CALLED IN FIXED UPDATE
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
            //GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpHeight);
            //if (jumpedHeight <= jumpHeight)
            //{
                //jumpedHeight += jumpSpeed * Time.deltaTime;
                transform.Translate(Vector2.up * jumpSpeed * Time.deltaTime);
            //}
        }

        if (Input.GetKey(KeyCode.Space) && coolDownTimer <= 0)
        {
            Attack();
        }
    }
    
    void OnDrawGizmo()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(foot.transform.position,groundCheckRadius);
    }

    private IEnumerator InvincibleIndicator()
    {
        while (isInvincible)
        {
            GetComponent<Renderer>().material.color = Color.yellow;
            yield return new WaitForSeconds(0.1f);
            GetComponent<Renderer>().material.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
        StopCoroutine("InvincibleIndicator");
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (damageSource.Contains(other.tag) && !isDying && !isTakingDamage)
        {
            if (isInvincible)
            {
                GetComponent<Animator>().SetTrigger("block");
            }
            else
            {
                isBeingPushed = true;

                if (other.tag == "EnemyMelee")
                {
                    Instantiate(Resources.Load("Prefabs/PlayerHittedByMelee"), transform.position, transform.rotation);
                    pushedDistance = other.GetComponentInParent<Enemy>().meleePushDistance;
                    isPushedToRight = other.GetComponentInParent<Enemy>().isFacingRight;
                    currentHealth -= other.transform.parent.GetComponent<Enemy>().meleeDamage;
                }
                else
                {
                    pushedDistance = other.GetComponent<MachineGunBullet>().pushDistance;
                    isPushedToRight = other.GetComponent<Rigidbody2D>().velocity.x > 0;
                    currentHealth -= other.GetComponent<MachineGunBullet>().damage;
                }
                
                healthBarUI.fillAmount = currentHealth / maxHealth;
                gameObject.GetComponent<Renderer>().material.color = Color.red;
                StartCoroutine("resetColor");
                if (currentHealth <= 0)
                {
                    isDying = true;
                    GetComponent<Animator>().SetTrigger("die");
                }
            }
        }
    }

    IEnumerator resetColor()
    {
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<Renderer>().material.color = Color.white;
        StopCoroutine("resetColor");
    }

    void Explode()
    {
        Instantiate(Resources.Load("Prefabs/PlayerDie"), transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
