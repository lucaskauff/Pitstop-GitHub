using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HHSpawnerFirstGlade : MonoBehaviour
{
    public bool spawnTheThing = true;
    public GameObject whatToSpawn;
    GameObject theSpawnedThing;
    bool spawnCheck = false;

    private void Start()
    {
        whatToSpawn = GameObject.Find("FirstHH");
    }

    private void Update()
    {
        if (spawnTheThing && !spawnCheck)
        {
            theSpawnedThing = (GameObject)Instantiate(whatToSpawn, transform.position, whatToSpawn.transform.rotation);

        }
    }
}