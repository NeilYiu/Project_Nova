using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    //public LoadingManager instance;
    public string levelName="";
    //public float animationTimer;
    //public float animationDuration = 5.0f;
    // Use this for initialization
	void Start ()
	{
	    //animationTimer = animationDuration;
	}
	
	// Update is called once per frame
	void Update () {
        if (SceneManager.GetActiveScene().name == "Loading")
        {

        }
        if (!SceneManager.GetActiveScene().name.Contains("Loading"))
	    {
	        return;
	    }

	    
	    //if (animationTimer > 0)
	    //{
	    //    animationTimer -= Time.deltaTime;
	    //}
	    //else
	    //{
     //       animationTimer = animationDuration;
     //       SceneManager.LoadScene(levelName);
	    //}
	}
}
