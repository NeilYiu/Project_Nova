using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Scroll : MonoBehaviour {
    public float scrollSpeed = -10;
    public float leftEdge;
    public float rightEdge;
    public bool canScroll=true;
    public float scrollSpeedIncrement = 0.1f;
    // Use this for initialization
    void Start ()
    {

    }

    void FixedUpdate()
    {
        
    }
    // Update is called once per frame
    void Update ()
    {
        //if (SceneManager.GetActiveScene().name == "Level4")
        //{
            scrollSpeed -= Time.fixedDeltaTime*scrollSpeedIncrement;
        //}
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
