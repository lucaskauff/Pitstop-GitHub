using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] GameObject whatToSpawn = default;
        [SerializeField] GameObject targetOfSpawnedThing = default;
        public GameObject theSpawnedThing = default;

        public void SpawnTheThing()
        {
            theSpawnedThing = (GameObject)Instantiate(whatToSpawn, transform.position, whatToSpawn.transform.rotation);
            theSpawnedThing.SetActive(true);
            theSpawnedThing.GetComponent<GorillaBehaviour>().target = targetOfSpawnedThing;
        }
    }
}