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
        [SerializeField] Animator energeticBarrier = default;
        [SerializeField] GameObject[] thingsToSpawn = default;
        [SerializeField] GameObject hammerheadToSpawn = default;
        [SerializeField] Transform[] spawnPoints = default;
        [SerializeField] int damageDealing = 1;
        [SerializeField] float radiusAroundBeast = 3;
        [SerializeField] float heightForSpawnedObjects = 5;
        [SerializeField] float objectsFallSpeed = 3;
        [SerializeField] float secondsToWaitAfterRocksSpawn = 8;
        [SerializeField] float secondsToWaitOnRageMode = 4;
        [SerializeField] float timePassedStayingVulnerable = 10;

        public GameObject[] spawnedObjectsOnScene;
        public GameObject[] hammerheadsOnScene;
        public bool isOnGround = false;

        bool fightCanStart = false;
        bool objectsHaveBeenSpawned = false;
        public bool canSpawnHammerheads = false;

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
                energeticBarrier.SetTrigger("CloseTheDoor");
            }
            else if (startBossFightDirectly)
            {
                WakeUp();
                energeticBarrier.SetTrigger("CloseTheDoor");
            }

            if (fightCanStart)
            {
                BossFightRoutines();
            }
        }

        void BossFightRoutines()
        {
            if (!objectsHaveBeenSpawned)
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

                canSpawnHammerheads = CanSpawnHammerHead(hammerheadsOnScene);

                spawnPoints[i].position = Random.insideUnitCircle * radiusAroundBeast + (Vector2)transform.position;
                Vector2 spawnPosHeight = spawnPoints[i].position + new Vector3(0, heightForSpawnedObjects, 0);
                int randomNumberForSpawn = Mathf.RoundToInt(Random.Range(0, thingsToSpawn.Length));

                if (!canSpawnHammerheads)
                {
                    hammerheadsOnScene = new GameObject[spawnPoints.Length];

                    if (spawnedObjectsOnScene[i] != null)
                    {
                        Destroy(spawnedObjectsOnScene[i]);
                    }

                    spawnedObjectsOnScene[i] = Instantiate(thingsToSpawn[randomNumberForSpawn], spawnPosHeight, thingsToSpawn[randomNumberForSpawn].transform.rotation);
                }
                else
                {
                    StopCoroutine(SlapFloor());
                    spawnedObjectsOnScene[i] = Instantiate(hammerheadToSpawn, spawnPoints[i].position, thingsToSpawn[randomNumberForSpawn].transform.rotation);
                    hammerheadsOnScene[i] = spawnedObjectsOnScene[i];
                    objectsHaveBeenSpawned = true;
                }

                switch (spawnedObjectsOnScene[i].tag)
                {
                    case "ObjectRock":
                        spawnedObjectsOnScene[i].GetComponent<RockBehaviour>().shouldGenerateImpulse = false;
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

                    case "Enemy":
                        spawnedObjectsOnScene[i].SetActive(true);
                        //spawnedObjectsOnScene[i].GetComponent<GorillaBehaviour>().target = spawnPoints[i].gameObject;
                        break;
                }
            }
        }

        private bool CanSpawnHammerHead(GameObject[] _goArray)
        {
            for (int i = 0; i < _goArray.Length; i++)
            {
                if (_goArray[i] != null)
                {
                    return false;
                }
            }

            return true;
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
                    if (!isOnGround)
                    {
                        isOnGround = true;
                        StartCoroutine(WaitBeforeGettingUp());
                    }
                    else
                    {
                        myHealthManager.HurtEnemy(1);
                        isOnGround = false;
                        StopCoroutine(WaitBeforeGettingUp());
                        //myAnim.SetTrigger("RealHit");
                    }

                    myAnim.SetBool("FirstHit", isOnGround);
                    Destroy(collision.gameObject);
                }
            }
        }

        IEnumerator SlapFloor()
        {
            objectsHaveBeenSpawned = true;

            yield return new WaitForSeconds(secondsToWaitAfterRocksSpawn);

            objectsHaveBeenSpawned = false;
        }

        IEnumerator WaitBeforeGettingUp()
        {
            yield return new WaitForSeconds(timePassedStayingVulnerable);
            isOnGround = false;
            myAnim.SetBool("FirstHit", isOnGround);
            StopCoroutine(WaitBeforeGettingUp());
        }
    }
}