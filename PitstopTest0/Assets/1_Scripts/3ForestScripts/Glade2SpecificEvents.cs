using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class Glade2SpecificEvents : MonoBehaviour
    {
        SceneLoader sceneLoader;

        [SerializeField] Transform targetForHammerHeadInsideG2 = default;
        [SerializeField] GameObject targetForHammerHeadOutsideG2 = default;
        [SerializeField] EnemySpawner enemySpawner = default;
        [SerializeField] Animator theFade = default;
        [SerializeField] float hammerheadSlowSpeed = 1f;
        [SerializeField] float slowDownLength = 5f;

        float hammerheadOriginalRushSpeed;
        bool triggerOnlyOnceCheck = false;

        public GorillaBehaviour hHGlade2Beh = null;
        bool hHStill = false;

        private void Start()
        {
            sceneLoader = GameManager.Instance.sceneLoader;
        }

        private void Update()
        {
            if (hHGlade2Beh != null)
            {
                if (hHGlade2Beh.gameObject.transform.position == targetForHammerHeadInsideG2.position && !hHStill)
                {
                    hHGlade2Beh.canMove = false;
                    hHStill = true;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Hammerhead(Clone)")
            {
                hHGlade2Beh = collision.gameObject.GetComponent<GorillaBehaviour>();
            }
            else if (collision.gameObject.tag == "Player")
            {
                if (hHGlade2Beh == null)
                {
                    enemySpawner.targetOfSpawnedThing = collision.gameObject;
                    enemySpawner.SpawnTheThing();
                    hHGlade2Beh = enemySpawner.theSpawnedThing.GetComponent<GorillaBehaviour>();
                    hHStill = true;
                }

                hHGlade2Beh.isFleeing = false;
                hHGlade2Beh.canMove = true;
                hammerheadOriginalRushSpeed = hHGlade2Beh.rushSpeed;

                //it is buggy
                if (!triggerOnlyOnceCheck)
                {
                    StartCoroutine(SlowingTheHammerheadDown());
                    triggerOnlyOnceCheck = true;
                }
            }
        }

        public void HammerheadIsOutOfGlade()
        {
            StopCoroutine(SlowingTheHammerheadDown());
            hHGlade2Beh.rushSpeed = hammerheadOriginalRushSpeed;
            hHGlade2Beh.target = targetForHammerHeadOutsideG2;
            hHGlade2Beh.isFleeing = true;
        }

        IEnumerator SlowingTheHammerheadDown()
        {
            hHGlade2Beh.rushSpeed = hammerheadSlowSpeed;
            yield return new WaitForSeconds(slowDownLength);

            if (!hHGlade2Beh.isFleeing)
            {
                theFade.SetTrigger("PlayerIsDead");
            }

            hHGlade2Beh.rushSpeed = hammerheadOriginalRushSpeed;
        }
    }
}