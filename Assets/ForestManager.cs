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
	// Use this for initialization
	void Start ()
	{
	    spawnEnemy = GameObject.Find("EnemyManager").GetComponent<SpawnEnemy>();
	    gameOverText = GameObject.Find("Canvas/GameOverText").GetComponent<Text>();
        gameOverText2 = GameObject.Find("Canvas/GameOverText/Text").GetComponent<Text>();
        gameOverText.enabled = false;
	    gameOverText2.enabled = false;
        sceneL = GameObject.Find("SceneL");
        sceneR = GameObject.Find("SceneR");
    }

    // Update is called once per frame
    void Update () {
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
            }
	        
	        if (Input.GetKeyDown(KeyCode.Space))
	        {
                spawnEnemy.isPlayerAlive = true;
                isStopped = false;
                Instantiate(Resources.Load("Prefabs/Boy"), transform.position, transform.rotation) ;
	            isPlayerAlive = true;
                gameOverText.enabled = false;
                gameOverText2.enabled = false;
                sceneL.GetComponent<Scroll>().canScroll = true;
                sceneR.GetComponent<Scroll>().canScroll = true;
            }
        }
    }
}
