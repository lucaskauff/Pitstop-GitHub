using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class HoleScript : MonoBehaviour
    {
        [SerializeField] Collider2D myCol = default;
        [SerializeField] float timePlayerHasToReact = 1;
        [SerializeField] PlayerControllerIso playerControllerIso = default;
        public GorillaBehaviour gorillaBehaviour = null;
        [SerializeField] Animator theFadeOnDead = default;

        bool delayIsPlaying = false;

        private void Update()
        {
            if (gorillaBehaviour == null)
            {
                if (playerControllerIso.isBeingRepulsed)
                {
                    myCol.isTrigger = true;
                }
                else
                {
                    myCol.isTrigger = false;
                }
            }
            else
            {
                if (playerControllerIso.isBeingRepulsed || gorillaBehaviour.isBeingRepulsed)
                {
                    myCol.isTrigger = true;
                }
                else
                {
                    myCol.isTrigger = false;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == playerControllerIso.gameObject.name)
            {
                if (!playerControllerIso.isBeingRepulsed)
                {                    
                    StartCoroutine(PlayerDelay());
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.name == playerControllerIso.gameObject.name)
            {
                StopAllCoroutines();
                return;
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.name == playerControllerIso.gameObject.name)
            {
                if (!delayIsPlaying && !playerControllerIso.isBeingRepulsed)
                {
                    HoleConsequence();
                }
            }
        }

        private void HoleConsequence()
        {
            playerControllerIso.canMove = false;
            theFadeOnDead.SetTrigger("PlayerIsDead");
        }

        IEnumerator PlayerDelay()
        {
            delayIsPlaying = true;

            if (playerControllerIso.isBeingRepulsed)
            {
                yield return null;
            }

            yield return new WaitForSeconds(timePlayerHasToReact);

            delayIsPlaying = false;
            HoleConsequence();
        }
    }
}