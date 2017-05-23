using UnityEngine;
using System.Collections;

public class AerialEnemyBullet : MonoBehaviour {
    private GameObject targetGO;
    private Vector3 dir;
    public float speed = 100;
    public float damage = 1;
    // Use this for initialization
    void Start () {

    }

    void Awake()
    {
        targetGO = GameObject.FindWithTag("Player");
        dir = (targetGO.transform.position - transform.position) /
                        (targetGO.transform.position - transform.position).magnitude;
    }

    // Update is called once per frame
    void Update () {
        transform.Translate(dir * speed * Time.deltaTime);
    }

    //void OnCollisionEnter2D(Collider2D other)
    //{
    //    if (other.tag == "Player")
    //    {
            
    //    }
    //}
}
