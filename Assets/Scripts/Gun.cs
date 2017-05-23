using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public GameObject center;
    public float speed;
    public Vector2 shootDirection;

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update ()
	{
	    shootDirection = transform.FindChild("Dir").gameObject.transform.position - transform.position;
	    if (Input.GetKey(KeyCode.LeftArrow))
	    {
            transform.RotateAround(center.transform.position, Vector3.forward, speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.RotateAround(center.transform.position, Vector3.back, speed * Time.deltaTime);
        }
    }
    
}
