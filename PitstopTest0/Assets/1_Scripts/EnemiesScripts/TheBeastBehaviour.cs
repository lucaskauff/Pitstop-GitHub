using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Pitstop
{
    public class TheBeastBehaviour : MonoBehaviour
    {
        public bool startBossFightDirectly = false;

        [Header("My components")]
        [SerializeField] Animator myAnim = default;
        [SerializeField] EnemyHealthManager myHealthManager = default;
        [SerializeField] CinemachineImpulseSource myImpulseSource = default;

        [Header("Serializable")]
        [SerializeField] DialogueManager dialogueManager = default;
        [SerializeField] GameObject triggerFightDirectly = default;
        [SerializeField] GameObject bossFightDialogue = default;
        [SerializeField] GameObject hammerheadToSpawn = default;
        [SerializeField] GameObject[] thingsToSpawn = default;
        [SerializeField] Transform[] spawnPoints = default;
        [SerializeField] int damageDealing = 1;
        [SerializeField] float radiusAroundBeast = 3;
        [SerializeField] float heightForSpawnedObjects = 5;
        [SerializeField] float objectsFallSpeed = 3;
        [SerializeField] float secondsToWaitAfterRocksSpawn = 8;

        public GameObject[] spawnedObjectsOnScene;
        bool fightCanStart = false;

        bool rocksHaveBeenSpawned = false;

        //ON THIS VERSION, BEAST WAKES UP AFTER DIALOGUE !

        private void Start()
        {
            spawnedObjectsOnScene = new GameObject[spawnPoints.Length];
        }

        private void Update()
        {
            if (dialogueManager.codeOfTheLastTriggeringSentence == "Start Boss Fight")
            {
                triggerFightDirectly.SetActive(false);
                bossFightDialogue.SetActive(false);

                WakeUp();
            }
            else if (startBossFightDirectly)
            {
                //bossFightDialogue.SetActive(false);

                WakeUp();
            }

            if (fightCanStart)
            {
                BossFightRoutines();
            }
        }

        void BossFightRoutines()
        {
            if (!rocksHaveBeenSpawned)
            {
                StopCoroutine(SlapFloor());
                myAnim.SetTrigger("HitFloor");
                StartCoroutine(SlapFloor());
            }
        }

        public void WakeUp()
        {
            myAnim.SetTrigger("WakeUp");
        }

        public void BeastIsAwoke()
        {
            fightCanStart = true;
        }

        public void FloorHasBeenHit()
        {
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                myImpulseSource.GenerateImpulse();

                if (spawnedObjectsOnScene[i] != null)
                {
                    Destroy(spawnedObjectsOnScene[i]);
                }

                spawnPoints[i].position = Random.insideUnitCircle * radiusAroundBeast + (Vector2)transform.position;
                Vector2 spawnPosHeight = spawnPoints[i].position + new Vector3(0, heightForSpawnedObjects, 0);
                int randomNumberForSpawn = Mathf.RoundToInt(Random.Range(0, thingsToSpawn.Length));

                spawnedObjectsOnScene[i] = Instantiate(thingsToSpawn[randomNumberForSpawn], spawnPosHeight, thingsToSpawn[randomNumberForSpawn].transform.rotation);

                switch (spawnedObjectsOnScene[i].tag)
                {
                    case "ObjectRock":
                        spawnedObjectsOnScene[i].GetComponent<ScannableObjectBehaviour>().targetPos = spawnPoints[i].position;
                        spawnedObjectsOnScene[i].GetComponent<ScannableObjectBehaviour>().projectileSpeed = objectsFallSpeed;
                        spawnedObjectsOnScene[i].GetComponent<ScannableObjectBehaviour>().isScannable = false;
                        spawnedObjectsOnScene[i].GetComponent<ScannableObjectBehaviour>().isFired = true;
                        break;

                    case "ObjectApple":
                        spawnedObjectsOnScene[i].GetComponent<ScannableObjectBehaviour>().targetPos = spawnPoints[i].position;
                        spawnedObjectsOnScene[i].GetComponent<ScannableObjectBehaviour>().projectileSpeed = objectsFallSpeed;
                        spawnedObjectsOnScene[i].GetComponent<ScannableObjectBehaviour>().isScannable = false;
                        spawnedObjectsOnScene[i].GetComponent<ScannableObjectBehaviour>().isFired = true;
                        break;

                    case "HookPoint":
                        spawnedObjectsOnScene[i].GetComponent<HookPointBehaviour>().targetPos = spawnPoints[i].position;
                        spawnedObjectsOnScene[i].GetComponent<HookPointBehaviour>().fallSpeed = objectsFallSpeed;
                        spawnedObjectsOnScene[i].GetComponent<HookPointBehaviour>().spawnedByBeast = true;
                        break;
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damageDealing);
            }
            else if (collision.gameObject.tag == "Enemy")
            {
                if (collision.gameObject.GetComponent<GorillaBehaviour>().isBeingRepulsed)
                {
                    myHealthManager.HurtEnemy(1);
                }
            }
        }

        IEnumerator SlapFloor()
        {
            rocksHaveBeenSpawned = true;

            yield return new WaitForSeconds(secondsToWaitAfterRocksSpawn);

            rocksHaveBeenSpawned = false;
        }
    }
}