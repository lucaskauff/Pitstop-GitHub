using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class Glade1SpecificEvents : MonoBehaviour
    {
        //Serializable
        [SerializeField] EnemySpawner hHNest = default;
        [SerializeField] GameObject targetForHammerHeadOutsideG1 = default;

        //Private
        bool playerEnteredCheck = false;
        GorillaBehaviour hHGlade1Beh;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player" && !playerEnteredCheck)
            {
                Debug.Log("Player entered Glade1.");
                hHNest.SpawnTheThing();
                hHGlade1Beh = hHNest.theSpawnedThing.GetComponent<GorillaBehaviour>();
                playerEnteredCheck = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject == hHGlade1Beh.gameObject)
            {
                hHGlade1Beh.target = targetForHammerHeadOutsideG1;
                hHGlade1Beh.isFleeing = true;
            }
        }
    }
}