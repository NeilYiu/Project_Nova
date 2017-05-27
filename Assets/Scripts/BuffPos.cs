using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuffPos : MonoBehaviour {
    public GameObject[] buffPrefabs;
    public float coolDownTimer = 0;
    public float maxCoolDown = 9;
    public float minCoolDown = 4;
    public List<GameObject> activeBuffs = new List<GameObject>();
    public bool isPlayerAlive = true;
    public bool isStopped = false;
    // Use this for initialization
    void Start () {
        coolDownTimer = Random.Range(minCoolDown, maxCoolDown);
    }
    void Update()
    {
        if (!isStopped && !isPlayerAlive)
        {
            isStopped = true;
            foreach (GameObject buff in activeBuffs)
            {
                if (buff.GetComponent<AerialMovementBuff>())
                    buff.GetComponent<AerialMovementBuff>().isPlayerAlive = false;

                if (buff.GetComponent<MoveForwardBuff>())
                    buff.GetComponent<MoveForwardBuff>().isPlayerAlive = false;

                if (buff.GetComponent<ProtectionBuff>())
                    buff.GetComponent<ProtectionBuff>().isPlayerAlive = false;

                if (buff.GetComponent<AxeBuff>())
                    buff.GetComponent<AxeBuff>().isPlayerAlive = false;
            }
        }

        if (coolDownTimer > 0)
        {
            coolDownTimer -= Time.deltaTime;
        }
        else
        {
            coolDownTimer = Random.Range(minCoolDown, maxCoolDown);
            Spawn();
        }
        if (isPlayerAlive)
        {
            foreach (GameObject buff in activeBuffs)
            {
                if (buff.gameObject.name == "MoveForwardBuff")
                {
                    if (buff.GetComponent<MoveForwardBuff>().isPlayerAlive == false)
                    {
                        buff.GetComponent<MoveForwardBuff>().isPlayerAlive = true;
                    }
                }
                if (buff.gameObject.name == "AerialMovementBuff")
                {
                    if (buff.GetComponent<AerialMovementBuff>().isPlayerAlive == false)
                    {
                        buff.GetComponent<AerialMovementBuff>().isPlayerAlive = true;
                    }
                }
                if (buff.gameObject.name == "ProtectionBuff")
                {
                    if (buff.GetComponent<ProtectionBuff>().isPlayerAlive == false)
                    {
                        buff.GetComponent<ProtectionBuff>().isPlayerAlive = true;
                    }
                }

                if (buff.gameObject.name == "AxeBuff")
                {
                    if (buff.GetComponent<AxeBuff>().isPlayerAlive == false)
                    {
                        buff.GetComponent<AxeBuff>().isPlayerAlive = true;
                    }
                }
            }
            isStopped = false;
        }

    }

    void Spawn()
    {
        if (isPlayerAlive)
        {
            foreach (GameObject buff in buffPrefabs)
            {
                GameObject temp = GameObject.Instantiate(buff, transform.position, Quaternion.identity) as GameObject;
                activeBuffs.Add(temp);
            }
        }
    }
}
