using UnityEngine;
using System.Collections;

public class Player : Character {
    //public float speed = 5f;
    public float arielSpeed = 3f;

    public float jumpHeight;
    public bool isGrounded;

    public Transform foot;
    public float groundCheckRadius;
    public LayerMask ground;
    
    // Use this for initialization
    public override void Start ()
    {
        isFacingRight = true;
        currentHealth = maxHealth;
        coolDown = bullet.GetComponent<MachineGunBullet>().coolDown;
    }

    public override void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(foot.transform.position, groundCheckRadius, ground);

        DetectInputs();

        base.FixedUpdate();
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
        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Animator>().SetBool("isWalking", true);

            if (isGrounded)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-arielSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
            if (!isFacingRight)
            {
                //Face to the moving direction
                ChangeDirection();
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Animator>().SetBool("isWalking", true);

            if (isGrounded)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(arielSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
            if (isFacingRight)
            {
                //Face to the moving direction
                ChangeDirection();
            }
        }
        else
        {
            GetComponent<Animator>().SetBool("isWalking", false);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        }


        if (Input.GetAxis("Jump") > 0 && isGrounded)
        {
            //AddForce BUG: ONLY BEHAVE THE SAME WHEN CALLED IN FIXED UPDATE
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
            //GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpHeight);
        }

        if (Input.GetKeyDown(KeyCode.Space) && coolDownTimer <= 0)
        {
            Attack();
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
