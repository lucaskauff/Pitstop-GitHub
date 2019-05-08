using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class Glade1SpecificEvents : MonoBehaviour
    {
        public bool spawnAnotherOne = false;

        //Serializable
        [SerializeField] EnemySpawner hHNest = default;
        [SerializeField] GameObject targetForHammerHeadOutsideG1 = default;
        [SerializeField] PlayerControllerIso playerControllerIso = default;

        //Private
        bool playerEnteredCheck = false;
        GorillaBehaviour hHGlade1Beh;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player" && !playerEnteredCheck)
            {
                hHNest.SpawnTheThing();
                hHGlade1Beh = hHNest.theSpawnedThing.GetComponent<GorillaBehaviour>();
                playerControllerIso.canMove = false;
                playerEnteredCheck = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject == hHGlade1Beh.gameObject)
            {
                hHGlade1Beh.target = targetForHammerHeadOutsideG1;
                hHGlade1Beh.isFleeing = true;
                playerControllerIso.canMove = true;

                if (spawnAnotherOne)
                {
                    SpawnAnotherHH();
                }
            }
        }

        private void SpawnAnotherHH()
        {
            hHNest.targetOfSpawnedThing = playerControllerIso.gameObject;
            hHNest.SpawnTheThing();
        }
    }
}