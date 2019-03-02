using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject whatToSpawn;
    GameObject theSpawnedThing;

    public void SpawnTheThing()
    {
        theSpawnedThing = (GameObject)Instantiate(whatToSpawn, transform.position, whatToSpawn.transform.rotation);
    }
}