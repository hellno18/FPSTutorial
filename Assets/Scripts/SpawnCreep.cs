using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCreep : MonoBehaviour {
    public Transform [] spawner;
    public float spawnTime=5f;
    public GameObject enemyprefap;
    public bool spawn;
    public int random;
    // Use this for initialization
    void Start () {
        spawn = true;
	}
	
	// Update is called once per frame
	void Update () {
        //random integer from 1 to 3
        random = Random.Range(1, 4);
        if (spawn)
        {
            spawn = false;
            //call spawnmonster function
            SpawnMonster();
        }	
	}
    void SpawnMonster()
    {
        if (random == 1)
        {
            Instantiate(enemyprefap, spawner[0].transform.position, Quaternion.identity);
        }
        if (random == 2)
        {
            Instantiate(enemyprefap, spawner[1].transform.position, Quaternion.identity);
        }
        if (random == 3)
        {
            Instantiate(enemyprefap, spawner[2].transform.position, Quaternion.identity);
        }


        //call ienumerator spawndelay to wait by spawnTime
        StartCoroutine(spawndelay());
    }
    IEnumerator spawndelay()
    {
        yield return new WaitForSeconds(spawnTime);
        spawn = true;
    }
}
