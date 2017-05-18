using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlatformGameManager : MonoBehaviour
{
    public Text timer;
    public float remainingTime = 0;
    public Transform camera;
    public Transform player;
	// Use this for initialization
	void Start ()
	{
	    timer = transform.Find("Timer").GetComponent<Text>();
	    remainingTime = float.Parse(timer.text);
	}
	
	// Update is called once per frame
	void Update ()
	{
	    remainingTime -= Time.deltaTime;
	    timer.text = remainingTime.ToString();
        camera = GameObject.FindWithTag("MainCamera").gameObject.transform;
	    player = GameObject.FindWithTag("Player").gameObject.transform;
	}
}
