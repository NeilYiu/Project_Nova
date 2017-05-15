using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IgnoreCollision : MonoBehaviour
{
    [SerializeField]
    private List<Collider2D> others;

    void Awake()
    {
        foreach (Collider2D other in others)
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), other, true);
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
