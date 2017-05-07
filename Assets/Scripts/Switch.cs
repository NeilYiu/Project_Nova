using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour
{
    private Animator anim;
    public bool isSticky;
	// Use this for initialization
	void Start ()
	{
	    anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnTriggerStay2D()
    {
        anim.SetBool("isTriggered", true);
    }

    void OnTriggerExit2D()
    {
        if (!isSticky)
        {
            anim.SetBool("isTriggered", false);
        }
    }
}
