using UnityEngine;
using System.Collections;

public class SelfDestruction : MonoBehaviour
{
    public float life;
	// Use this for initialization
	void Awake ()
	{
	    Destroy(gameObject, life);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
