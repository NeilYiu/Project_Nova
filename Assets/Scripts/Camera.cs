using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour
{

    public Transform target;
    public float smoothing;

    public Vector3 offset;
    public float yMin;
	// Use this for initialization
	void Start ()
	{
        //put some z distance between cam and player
        offset = transform.position - target.position;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    Vector3 camDestination = target.position + offset;
        //Wrong usage with special smoothing effect
	    transform.position = Vector3.Lerp(transform.position, camDestination, smoothing*Time.deltaTime);
        //When player fall 
        if (transform.position.y < yMin)
        {
            transform.position = new Vector3(transform.position.x, yMin, transform.position.z);
        }
    }
}
