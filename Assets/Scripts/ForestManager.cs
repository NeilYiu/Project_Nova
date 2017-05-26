using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ForestManager : MonoBehaviour
{
    public Text gameOverText;
    public Text gameOverText2;
    public GameObject sceneL;
    public GameObject sceneR;
    public SpawnEnemy spawnEnemy;
    public bool isPlayerAlive = true;
    public bool isStopped = false;
    public GameObject player;
    public BuffManager buffManager;
	// Use this for initialization
	void Start ()
	{
        spawnEnemy = GameObject.Find("EnemyManager").GetComponent<SpawnEnemy>();
        buffManager = GameObject.Find("BuffManager").GetComponent<BuffManager>();

        gameOverText = GameObject.Find("Canvas/GameOverText").GetComponent<Text>();
        gameOverText2 = GameObject.Find("Canvas/GameOverText/Text").GetComponent<Text>();
        player = GameObject.FindWithTag("Player");
        gameOverText.enabled = false;
	    gameOverText2.enabled = false;
        sceneL = GameObject.Find("SceneL");
        sceneR = GameObject.Find("SceneR");
    }

    // Update is called once per frame
    void Update () {
        if (player != null)
        {
            if (player.transform.position.x < -70.0f)
            {
                isPlayerAlive = false;
                Destroy(player);
            }
        }

        if (!isPlayerAlive)
	    {
	        if (!isStopped)
	        {
	            isStopped = true;
                sceneL.GetComponent<Scroll>().canScroll = false;
                sceneR.GetComponent<Scroll>().canScroll = false;
                gameOverText2.enabled = true;
                gameOverText.enabled = true;
                spawnEnemy.isPlayerAlive = false;
                buffManager.isPlayerAlive = false;
	        }
	        
	        if (Input.GetKeyDown(KeyCode.Space))
	        {
                spawnEnemy.isPlayerAlive = true;
                buffManager.isPlayerAlive = true;
                isStopped = false;
                Instantiate(Resources.Load("Prefabs/Boy"), transform.position, transform.rotation) ;
	            isPlayerAlive = true;
                gameOverText.enabled = false;
                gameOverText2.enabled = false;
                player = GameObject.FindWithTag("Player");
                sceneL.GetComponent<Scroll>().canScroll = true;
                sceneR.GetComponent<Scroll>().canScroll = true;
            }
        }
    }
}
