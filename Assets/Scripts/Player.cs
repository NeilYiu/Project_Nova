using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public float speed = 5f;
    public float arielSpeed = 3f;

    public Vector2 jumpVector;
    public bool isGrounded;

    public Transform foot;
    public float radius;
    public LayerMask ground;

    public Transform gun;
    public GameObject bullet;
    public float fireRate = 0.5f;
    public float nextFire = 0f;
    public bool isFacingRight;
    public bool isUsingShotgun=false;
    // Use this for initialization
    void Start ()
    {
        isFacingRight = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A))
        {
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
            //transform.localScale = new Vector3(-1,1,1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
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
            //transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        }

	    isGrounded = Physics2D.OverlapCircle(foot.transform.position, radius, ground);

        if (Input.GetKey(KeyCode.W) && isGrounded)
        {
            GetComponent<Rigidbody2D>().AddForce(jumpVector, ForceMode2D.Force);
        }

	    if (Input.GetAxisRaw("Fire1")>0)
        {
            gun.transform.localPosition = new Vector3(0.55f, gun.transform.localPosition.y, gun.transform.localPosition.z);
            if (Time.time > nextFire)
	        {
	            nextFire = Time.time + fireRate;
	        }
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
	            gun.transform.localPosition = new Vector3(-0.8f,gun.transform.localPosition.y, gun.transform.localPosition.z);
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
        Gizmos.DrawWireSphere(foot.transform.position,radius);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Trap")
        {
            //TODO Reduce health
        }
    }
}
