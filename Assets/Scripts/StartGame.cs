using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public bool shouldFade;
    public Text startText;
	// Use this for initialization
	void Start ()
	{
	    startText = GameObject.Find("Canvas/SpaceText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (SceneManager.GetActiveScene().name.Contains("Level"))
	        return;

        if (startText == null )
	    {
            startText = GameObject.Find("Canvas/SpaceText").GetComponent<Text>();
        }
        if (startText.color.a <= 0)
	    {
	        shouldFade = false;
	    }
	    if(startText.color.a >= 1)
	    {
            shouldFade = true;
        }

	    if (shouldFade)
	    {
	        startText.color = new Color(255, 255, 255, startText.color.a - 0.01f);
	    }
	    else
	    {
            startText.color = new Color(255, 255, 255, startText.color.a + 0.01f);
        }
        if (Input.GetKey(KeyCode.Space))
	    {
            DontDestroyOnLoad(GameObject.Find("LoadingManager"));
            DontDestroyOnLoad(gameObject);

	        if (SceneManager.GetActiveScene().name == "StartScene")
	        {
                GameObject.Find("LoadingManager").GetComponent<LoadingManager>().levelName = "Level1";
                SceneManager.LoadScene("Loading");
            }
            if (SceneManager.GetActiveScene().name == "Loading")
            {
                GameObject.Find("LoadingManager").GetComponent<LoadingManager>().levelName = "Level1";
                SceneManager.LoadScene("Level1");
            }
            if (SceneManager.GetActiveScene().name == "Loading2")
            {
                GameObject.Find("LoadingManager").GetComponent<LoadingManager>().levelName = "Level1";
                SceneManager.LoadScene("Level2");
            }
            if (SceneManager.GetActiveScene().name == "Loading3")
            {
                GameObject.Find("LoadingManager").GetComponent<LoadingManager>().levelName = "Level1";
                SceneManager.LoadScene("Level3");
            }
            if (SceneManager.GetActiveScene().name == "Loading4")
            {
                GameObject.Find("LoadingManager").GetComponent<LoadingManager>().levelName = "Level1";
                SceneManager.LoadScene("Level4");
            }
        }
	}
}
