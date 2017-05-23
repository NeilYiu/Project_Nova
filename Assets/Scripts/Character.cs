using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour {
    public float speed = 5f;
    public Transform gun;
    public GameObject bullet;
    public float coolDown;
    public float coolDownTimer;
    public bool isFacingRight;
    public float maxHealth = 10;
    public float currentHealth;
    public bool isAttacking = false;
    public bool isUsingShotgun = false;
    public bool isMelee;
    public bool isTakingDamage;
    public static Player instance;
    public bool isDying;
    [SerializeField]
    public EdgeCollider2D meleeCollider;
    [SerializeField]
    public List<string> damageSource;

    public static Player Instance
    {
        get { return instance ?? (instance = GameObject.FindObjectOfType<Player>()); }
    }

    // Use this for initialization
    public virtual void Start ()
    {
        meleeCollider.enabled = false;
        currentHealth = maxHealth;
        if (SceneManager.GetActiveScene().name != "Scene2")
        {
            coolDown = bullet.GetComponent<MachineGunBullet>().coolDown;
        }
        isFacingRight = transform.localScale.x > 0;
    }

    public virtual void FixedUpdate()
    {
        if (coolDownTimer > 0)
        {
            coolDownTimer -= Time.fixedDeltaTime;
        }
        //if (isAttacking)
        //{
        //    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //}
    }
    public void MeleeAttack()
    {
        meleeCollider.enabled = !meleeCollider.enabled;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        
    }
    // Update is called once per frame
    public virtual void Update () {
	
	}

    public virtual void ChangeDirection()
    {
        isFacingRight = !isFacingRight;
        if (SceneManager.GetActiveScene().name != "Scene2")
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            
        }
        //GameObject gun = transform.FindChild("Gun").gameObject;
        //gun.transform.eulerAngles = new Vector3(gun.transform.eulerAngles.x, gun.transform.eulerAngles.y, gun.transform.eulerAngles.z);
    }

    //Shoot the bullet after attack
    public void Shoot()
    {
        if (SceneManager.GetActiveScene().name != "Scene2")
        {
            if (!isMelee)
            {
                if (isFacingRight)
                {
                    if (isUsingShotgun)
                    {
                        Instantiate(bullet, gun.position, Quaternion.Euler(new Vector3(0, 90, 0)));
                    }
                    else
                    {
                        Instantiate(bullet, gun.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                    }
                }
                else
                {
                    if (isUsingShotgun)
                    {
                        Instantiate(bullet, gun.position, Quaternion.Euler(new Vector3(180, 90, 0)));
                    }
                    else
                    {
                        Instantiate(bullet, gun.position, Quaternion.Euler(new Vector3(0, 0, 180)));
                    }
                }
            }
        }
        else
        {
            bullet.GetComponent<SelfDestruction>().life = 100;
            bullet.GetComponent<MachineGunBullet>().shootDirection =
                transform.FindChild("Gun").GetComponent<Gun>().shootDirection;
            bullet.transform.rotation = transform.FindChild("Gun").transform.rotation;
            if (isFacingRight)
            {
                if (isUsingShotgun)
                {
                    Instantiate(bullet, gun.position, Quaternion.Euler(new Vector3(0, 90, 0)));
                }
                else
                {
                    Instantiate(bullet, gun.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                }
            }
            else
            {
                if (isUsingShotgun)
                {
                    Instantiate(bullet, gun.position, Quaternion.Euler(new Vector3(180, 90, 0)));
                }
                else
                {
                    Instantiate(bullet, gun.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                }
            }
        }
    }

    public void Attack()
    {
        GetComponent<Animator>().SetTrigger("attack");
        coolDownTimer = coolDown;
        if (SceneManager.GetActiveScene().name != "Scene2")
        {
            gun.transform.localPosition = new Vector3(0.18f, gun.transform.localPosition.y,
                gun.transform.localPosition.z);
        }
        else
        {
            
        }
    }
}
