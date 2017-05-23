using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AerialEnemy : MonoBehaviour {
    private GameObject targetGO;
    public float currentDistance = 0;
    public float attackDis = 2;
    public float attackRate = 2; //How many second it takes to attack once
    private float attackTimer = 0;
    public float speed = 2;
    public int life=3;
    public int coins = 10;
    public Text coinText;

    // Use this for initialization
    void Start () {
        coinText = GameObject.Find("PlayerStats/CoinsNum").GetComponent<Text>();
        targetGO = GameObject.FindWithTag("Player");
        InvokeRepeating("CalcDistance", 0, 0.1f);
    }
    void CalcDistance()
    {
        Transform player = targetGO.transform;
        currentDistance = Vector3.Distance(player.position, transform.position);
    }
    // Update is called once per frame
    void Update ()
    {
        if (currentDistance > attackDis)
        {
            Vector3 dir = (targetGO.transform.position - transform.position) /
                          (targetGO.transform.position - transform.position).magnitude;
            transform.Translate(dir * speed * Time.deltaTime);
        }
        else
        {
            attackTimer += Time.deltaTime;
            if (attackTimer > attackRate)
            {
                //TODO Attack!
                Instantiate(Resources.Load("Prefabs/Bullets/AerialEnemyBullet"), transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                attackTimer = 0;
            }
            //TODO Idle animation between each attack

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet" || other.gameObject.tag == "ShotgunBullet")
        {
            life -= 1;
            if (life<=0)
            {
                Instantiate(Resources.Load("Prefabs/MobDie"), transform.position, transform.rotation);
                int coinNum = int.Parse(coinText.text);
                coinNum += coins;
                coinText.text = coinNum.ToString();
                GameObject.Destroy(gameObject);
            }
            GameObject.Destroy(other.gameObject);

        }
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().currentHealth -= 1;
        }
    }
}
