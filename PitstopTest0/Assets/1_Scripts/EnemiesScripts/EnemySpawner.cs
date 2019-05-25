using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] GameObject whatToSpawn = default;
        public GameObject targetOfSpawnedThing = default;
        [HideInInspector] public GameObject theSpawnedThing = default;

        public void SpawnTheThing()
        {
            theSpawnedThing = (GameObject)Instantiate(whatToSpawn, transform.position, whatToSpawn.transform.rotation);
            theSpawnedThing.SetActive(true);
            theSpawnedThing.GetComponent<GorillaBehaviour>().target = targetOfSpawnedThing;
            //theSpawnedThing.GetComponent<GorillaBehaviour>().canMove = true;
        }
    }
}