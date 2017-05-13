using UnityEngine;
using System.Collections;

public class IgnoreCollision : MonoBehaviour
{
    [SerializeField]
    private Collider2D other;
    void Awake()
    {
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(),other,true);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
