using UnityEngine;
using System.Collections;

public class MachineGunBullet : Bullet
{
    public float bulletSpeed;
    public float damage;
    public float coolDown = 0.2f;
    public float pushDistance;
    public float countsOfUnitDistance = 3;

    // Use this for initialization
    void Awake ()
	{
	    if (transform.localRotation.z>0)
	    {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 0) * bulletSpeed, ForceMode2D.Impulse);
        }
	    else
	    {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0) * bulletSpeed, ForceMode2D.Impulse);
        }
        pushDistance = GetComponent<Renderer>().bounds.size.x * countsOfUnitDistance;
    }

    // Update is called once per frame
    void Update () {
	
	}
}
