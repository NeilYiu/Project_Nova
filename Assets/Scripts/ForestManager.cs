using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
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
    public Text distanceText;
    private AsyncOperation ao;
    private bool isLoading; 

	// Use this for initialization
	void Start ()
	{
        spawnEnemy = GameObject.Find("EnemyManager").GetComponent<SpawnEnemy>();
        buffManager = GameObject.Find("BuffManager").GetComponent<BuffManager>();
	    distanceText = GameObject.Find("Canvas/DistanceNumber").GetComponent<Text>();
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
            float distanceRemained = float.Parse(distanceText.text) - Time.deltaTime;

            if (distanceRemained > 0)
            {
                distanceText.text = distanceRemained.ToString("F2");
            }
            else if(!isLoading)
            {
                DontDestroyOnLoad(GameObject.Find("LoadingManager"));

                if (SceneManager.GetActiveScene().name == "Level1")
                {
                    GameObject.Find("LoadingManager").GetComponent<LoadingManager>().levelName = "Level2";
                    SceneManager.LoadScene("Loading2");
                }
                if (SceneManager.GetActiveScene().name == "Level2")
                {
                    GameObject.Find("LoadingManager").GetComponent<LoadingManager>().levelName = "Level3";
                    SceneManager.LoadScene("Loading3");
                }
                if (SceneManager.GetActiveScene().name == "Level3")
                {
                    GameObject.Find("LoadingManager").GetComponent<LoadingManager>().levelName = "Level4";
                    SceneManager.LoadScene("Loading4");
                }
            }

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
	            if (SceneManager.GetActiveScene().name == "Level4")
	            {
	                DontDestroyOnLoad(GameObject.Find("LoadingManager"));
	                //DontDestroyOnLoad(gameObject);
	                GameObject.Find("LoadingManager").GetComponent<LoadingManager>().levelName = "Level4";
	                SceneManager.LoadScene("Loading4");
	            }
	            else
	            {
                    spawnEnemy.isPlayerAlive = true;
                    buffManager.isPlayerAlive = true;
                    isStopped = false;
                    Instantiate(Resources.Load("Prefabs/Boy"), transform.position, transform.rotation);
                    isPlayerAlive = true;
                    gameOverText.enabled = false;
                    gameOverText2.enabled = false;
                    player = GameObject.FindWithTag("Player");
	                if (SceneManager.GetActiveScene().name == "Level4")
	                {
	                    player.GetComponent<Boy>().currentHealth = 5;
	                    GameObject.Find("Canvas/CurrentHealth").GetComponent<Text>().text = "5";
	                }
	                else
	                {
                        player.GetComponent<Boy>().currentHealth = 1;
                    }
                    sceneL.GetComponent<Scroll>().canScroll = true;
                    sceneR.GetComponent<Scroll>().canScroll = true;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            DontDestroyOnLoad(GameObject.Find("LoadingManager"));
            //DontDestroyOnLoad(gameObject);
            GameObject.Find("LoadingManager").GetComponent<LoadingManager>().levelName = "Level1";
            SceneManager.LoadScene("Loading");
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            DontDestroyOnLoad(GameObject.Find("LoadingManager"));
            //DontDestroyOnLoad(gameObject);
            GameObject.Find("LoadingManager").GetComponent<LoadingManager>().levelName = "Level2";
            SceneManager.LoadScene("Loading2");
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            DontDestroyOnLoad(GameObject.Find("LoadingManager"));
            //DontDestroyOnLoad(gameObject);
            GameObject.Find("LoadingManager").GetComponent<LoadingManager>().levelName = "Level3";
            SceneManager.LoadScene("Loading3");
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            DontDestroyOnLoad(GameObject.Find("LoadingManager"));
            //DontDestroyOnLoad(gameObject);
            GameObject.Find("LoadingManager").GetComponent<LoadingManager>().levelName = "Level4";
            SceneManager.LoadScene("Loading4");
        }
    }
}
