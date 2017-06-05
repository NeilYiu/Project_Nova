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
	    if (GetComponent<AudioSource>().clip!=null && !GetComponent<AudioSource>().isPlaying)
	    {
            GetComponent<AudioSource>().Play();
        }

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
            GetComponent<AudioSource>().Stop();
            if (SceneManager.GetActiveScene().name == "Loading")
            {
                GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Sound/TutorialLevels");
            }
            if (SceneManager.GetActiveScene().name == "Loading2")
            {
                GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Sound/TutorialLevels");
            }
            if (SceneManager.GetActiveScene().name == "Loading3")
            {
                GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Sound/Level3");
            }
            if (SceneManager.GetActiveScene().name == "Loading4")
            {
                GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Sound/Level4");
            }

            if (SceneManager.GetActiveScene().name == "StartScene")
	        {
                SceneManager.LoadScene("Loading");
            }
            if (SceneManager.GetActiveScene().name == "Loading")
            {
                SceneManager.LoadScene("Level1");
            }
            if (SceneManager.GetActiveScene().name == "Loading2")
            {
                SceneManager.LoadScene("Level2");
            }
            if (SceneManager.GetActiveScene().name == "Loading3")
            {
                SceneManager.LoadScene("Level3");
            }
            if (SceneManager.GetActiveScene().name == "Loading4")
            {
                SceneManager.LoadScene("Level4");
            }
        }
	}
}
