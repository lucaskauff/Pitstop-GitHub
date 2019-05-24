using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class TheBeastBehaviour : MonoBehaviour
    {
        public bool startBossFightDirectly = false;

        [SerializeField] Animator myAnim = default;
        [SerializeField] DialogueManager dialogueManager = default;
        [SerializeField] GameObject triggerFightDirectly = default;
        [SerializeField] GameObject bossFightDialogue = default;
        [SerializeField] GameObject hammerheadToSpawn = default;
        [SerializeField] GameObject[] rocksToSpawn = default;
        [SerializeField] GameObject hookpointToSpawn = default;
        [SerializeField] Transform[] spawnPoints = default;
        [SerializeField] float radiusAroundBeast = 3;
        [SerializeField] float secondsToWaitAfterRocksSpawn = 8;

        GameObject[] rocksOnScene;
        GameObject[] hammerheadsOnScene;
        bool fightCanStart = false;

        bool rocksHaveBeenSpawned = false;

        private void Update()
        {
            if (dialogueManager.codeOfTheLastTriggeringSentence == "Start Boss Fight" || startBossFightDirectly)
            {
                fightCanStart = true;
                triggerFightDirectly.SetActive(false);
                bossFightDialogue.SetActive(false);
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
                StartCoroutine(SlapFloor());
            }
        }

        public void WakeUp()
        {
            myAnim.SetTrigger("WakeUp");
        }

        IEnumerator SlapFloor()
        {
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                spawnPoints[i].position = Random.insideUnitCircle * radiusAroundBeast + (Vector2)transform.position;
                rocksOnScene[i] = Instantiate(rocksToSpawn[Mathf.RoundToInt(Random.Range(0, rocksToSpawn.Length-1))], spawnPoints[i].position, rocksToSpawn[i].transform.rotation);
            }

            rocksHaveBeenSpawned = true;

            yield return new WaitForSeconds(secondsToWaitAfterRocksSpawn);

            rocksHaveBeenSpawned = false;
        }
    }
}