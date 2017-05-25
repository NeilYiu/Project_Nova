using UnityEngine;
using System.Collections;

public class Scroll : MonoBehaviour {
    public float scrollSpeed = -10;
    public float leftEdge;
    public float rightEdge;
    public bool canScroll=true;
    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
	{
	    if (canScroll)
	    {
            transform.Translate(new Vector2(scrollSpeed, 0));
            if (gameObject.transform.position.x <= leftEdge)
            {
                gameObject.transform.position = new Vector3(rightEdge, .0f, 0.0f);
            }
        }
	}
}
