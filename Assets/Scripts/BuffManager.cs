using UnityEngine;
using System.Collections;

public class BuffManager : MonoBehaviour
{
    public GameObject[] buffPos;
    public bool isPlayerAlive = true;
    public bool isStopped;

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (!isStopped && !isPlayerAlive)
        {
            isStopped = true;
            foreach (GameObject pos in buffPos)
            {
                pos.gameObject.GetComponent<BuffPos>().isPlayerAlive = false;
                foreach (var buff in pos.GetComponent<BuffPos>().activeBuffs)
                {
                    if (pos.gameObject.name == "BuffPos1")
                        buff.GetComponent<AerialMovementBuff>().isPlayerAlive = false;

                    if (pos.gameObject.name == "BuffPos2")
                        buff.GetComponent<MoveForwardBuff>().isPlayerAlive = false;
                }
            }
        }
        if (isPlayerAlive && isStopped)
        {
            foreach (GameObject pos in buffPos)
            {
                pos.gameObject.GetComponent<BuffPos>().isPlayerAlive = true;
                foreach (var buff in pos.GetComponent<BuffPos>().activeBuffs)
                {
                    if (pos.gameObject.name == "BuffPos1")
                        buff.GetComponent<AerialMovementBuff>().isPlayerAlive = true;

                    if (pos.gameObject.name == "BuffPos2")
                        buff.GetComponent<MoveForwardBuff>().isPlayerAlive = true;
                   
                }
            }
            isStopped = false;
        }
    }
}
