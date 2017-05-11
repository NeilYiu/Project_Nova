using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public float speed = 5f;
    public float arielSpeed = 3f;

    public float jumpHeight;
    public bool isGrounded;

    public Transform foot;
    public float groundCheckRadius;
    public LayerMask ground;

    public Transform gun;
    public GameObject bullet;
    public float coolDown;
    private float coolDownTimer;

    public bool isFacingRight;
    public bool isUsingShotgun=false;

    public float maxHealth = 10;
    public float currentHealth;

    //public float attackTime, attackDelay = 3f;
    //public bool isAttacking = false;

    // Use this for initialization
    void Start ()
    {
        isFacingRight = true;
        currentHealth = maxHealth;
        coolDown = bullet.GetComponent<MachineGunBullet>().coolDown;
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(foot.transform.position, groundCheckRadius, ground);
        if (coolDownTimer > 0)
        {
            coolDownTimer -= Time.fixedDeltaTime;
        }
        DetectInputs();
    }

    // Update is called once per frame
    void Update () {
        //DetectInputs();
        if (currentHealth<=0)
        {
            Instantiate(Resources.Load("Prefabs/PlayerDie"),gameObject.transform.position, Quaternion.Euler(new Vector3(270, 0, 0)));
            Destroy(gameObject);
        }
    }

    private void DetectInputs()
    {
        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Animator>().SetBool("isWalking",true);
            isFacingRight = false;
            if (isGrounded)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-arielSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
            //Face to the moving direction
            transform.localScale = new Vector3(-6, 6, 1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Animator>().SetBool("isWalking", true);

            isFacingRight = true;
            if (isGrounded)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(arielSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
            //Face to the moving direction
            transform.localScale = new Vector3(6, 6, 1);
        }
        else
        {
            GetComponent<Animator>().SetBool("isWalking", false);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        }


        if (Input.GetAxis("Jump") > 0 && isGrounded)
        {
            //AddForce bug: ONLY BEHAVE THE SAME WHEN CALLED IN FIXED UPDATE
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
            //GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpHeight);
        }

        if (Input.GetKeyDown(KeyCode.Space) && coolDownTimer <= 0)
        {
            GetComponent<Animator>().SetTrigger("Attacking");
            //attackTime = attackDelay;
            coolDownTimer = coolDown;
            gun.transform.localPosition = new Vector3(0.07f, gun.transform.localPosition.y,
                gun.transform.localPosition.z);
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
                gun.transform.localPosition = new Vector3(-0.07f, gun.transform.localPosition.y,
                    gun.transform.localPosition.z);
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

    void OnDrawGizmo()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(foot.transform.position,groundCheckRadius);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Trap")
        {
            //TODO Reduce health
        }
    }

}
