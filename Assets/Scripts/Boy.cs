﻿using UnityEngine;
using System.Collections;
using System.Timers;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boy : MonoBehaviour {
    public float jumpHeight = 6;
    public bool isGrounded;
    public Transform foot;
    public float groundCheckRadius;
    public LayerMask ground;
    public float jumpedHeight = 0;
    [SerializeField]
    private float jumpSpeed = 5;
    public float speed = 5f;
    public float maxHealth = 5;
    public float currentHealth;
    public bool isMelee;
    public bool isTakingDamage;
    public bool isInvincible = false;
    public float invincibleTimer;
    public Text aerialMovementTimeText;
    public Text aerialMovementTypeText;

    public Text invincibleTimeText;
    public Text buffTypeText;
    public Text axeCount;
    public float axeCoolDown = 0.1f;
    //public bool canAerialMove = false;
    //public float aerialMoveTime;
    public float aerialMoveTimer;
    // Use this for initialization
    void Start()
    {
        invincibleTimeText = GameObject.Find("Canvas/BuffTime").GetComponent<Text>();
        //buffTypeText = GameObject.Find("Canvas/BuffType").GetComponent<Text>();
        axeCount = GameObject.Find("Canvas/AxeCount").GetComponent<Text>();
        aerialMovementTimeText = GameObject.Find("Canvas/AerialMoementTime").GetComponent<Text>();
        aerialMovementTypeText = GameObject.Find("Canvas/AerialMoementBuff").GetComponent<Text>();
    }
    void FixedUpdate()
    {
        if (axeCoolDown>0)
        {
            axeCoolDown -= Time.deltaTime;
        }
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            invincibleTimeText.text = invincibleTimer.ToString("F2");
        }
        if (invincibleTimer <= 0)
        {
            invincibleTimeText.text = "0";
            isInvincible = false;
        }

        if (aerialMoveTimer <= 0)
        {
            aerialMovementTimeText.text = "0";
        }
        

        isGrounded = Physics2D.OverlapCircle(foot.transform.position, groundCheckRadius, ground);

        if (aerialMoveTimer > 0)
        {
            aerialMoveTimer -= Time.deltaTime;
            aerialMovementTimeText.text = aerialMoveTimer.ToString("F2");
            DetectAerialInputs();
        }
        else
        {
            DetectInputs();
        }

        if (int.Parse(axeCount.text)>0)
        {
            if (Input.GetKey(KeyCode.M) && axeCoolDown<=0)
            {
                axeCoolDown = 0.3f;
                axeCount.text = (int.Parse(axeCount.text) - 1).ToString();
                Instantiate(Resources.Load("Prefabs/Axe"),transform.position, Quaternion.identity);
            }
        }
    }

    void DetectAerialInputs()
    {

        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
        }
        else if (Input.GetKey(KeyCode.D) && transform.position.x <= 3)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
        }
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpHeight);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            GameObject.Find("GameManager").GetComponent<ForestManager>().isPlayerAlive = false;
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
