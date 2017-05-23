using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {

    public GameObject[] enemyPrefabs;
    public Transform[] spawnPosArray;
    public float startOverTime = 8;  //Spawn after how many seconds
    public float coolDown = 1;
    // Use this for initialization
    void Start () {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            foreach (GameObject go in enemyPrefabs)
            {
                foreach (Transform t in spawnPosArray)
                {
                    GameObject temp = GameObject.Instantiate(go, t.position, Quaternion.identity) as GameObject;
                }
                yield return new WaitForSeconds(coolDown);

            }
            yield return new WaitForSeconds(startOverTime);
        }
    }
}
