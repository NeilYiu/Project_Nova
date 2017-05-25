﻿using UnityEngine;
using System.Collections;

public class Boy : MonoBehaviour {
    public float arielSpeed = 3f;
    public float jumpHeight = 6;
    public bool isGrounded;
    public Transform foot;
    public float groundCheckRadius;
    public LayerMask ground;
    public float jumpedHeight = 0;
    [SerializeField]
    private float jumpSpeed = 5;
    public float speed = 5f;
    public float maxHealth = 10;
    public float currentHealth;
    public bool isMelee;
    public bool isTakingDamage;
    public bool isInvincible = false;
    public float invincibleTime = 10f;
    public float invincibleTimer;

    // Use this for initialization
    void Start()
    {
        
    }
    void FixedUpdate()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
        }
        if (invincibleTimer <= 0)
        {
            isInvincible = false;
            invincibleTimer = invincibleTime;
        }
        isGrounded = Physics2D.OverlapCircle(foot.transform.position, groundCheckRadius, ground);
        
        DetectInputs();
    }
   
    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Instantiate(Resources.Load("Prefabs/PlayerDie"), gameObject.transform.position, Quaternion.Euler(new Vector3(270, 0, 0)));
            Destroy(gameObject);
        }
    }

    private void DetectInputs()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            //AddForce BUG: ONLY BEHAVE THE SAME WHEN CALLED IN FIXED UPDATE
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpHeight);
        }
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
    
    void Explode()
    {
        Instantiate(Resources.Load("Prefabs/PlayerDie"), transform.position, transform.rotation);
        Destroy(gameObject);
    }
}