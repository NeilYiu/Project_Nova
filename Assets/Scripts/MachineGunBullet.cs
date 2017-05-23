using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MachineGunBullet : Bullet
{
    public float bulletSpeed;
    public float damage;
    public float coolDown = 0.2f;
    public float pushDistance;
    public float countsOfUnitDistance = 3;
    public Vector2 shootDirection;

    // Use this for initialization
    void Awake()
    {
        if (SceneManager.GetActiveScene().name != "Scene2")
        {
            if (gameObject.tag == "Bullet")
            {
                GetComponent<SelfDestruction>().life = 0.15f;
            }
            if (transform.localRotation.z > 0)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 0) * bulletSpeed, ForceMode2D.Impulse);
            }
            else
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0) * bulletSpeed, ForceMode2D.Impulse);
            }
            pushDistance = GetComponent<Renderer>().bounds.size.x * countsOfUnitDistance;
        }
        else
        {
            GetComponent<SelfDestruction>().life = 4;
            GetComponent<Rigidbody2D>().AddForce(shootDirection * bulletSpeed/4, ForceMode2D.Impulse);
        }
    }


    // Update is called once per frame
    void Update () {
	
	}
}
