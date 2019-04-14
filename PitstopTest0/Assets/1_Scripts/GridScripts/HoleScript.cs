using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class HoleScript : MonoBehaviour
    {
        SceneLoader sceneLoader;

        [SerializeField] float timePlayerHasToReact = 1;
        [SerializeField] PlayerControllerIso playerControllerIso = default;
        //[SerializeField] GameObject player = default;

        private bool playerReallyFell = false;

        private void Start()
        {
            sceneLoader = GameManager.Instance.sceneLoader;
        }

        private void Update()
        {
            if (playerReallyFell)
            {
                HoleConsequence();
            }
            else
            {
                StopCoroutine(PlayerDelay());
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

        private void HoleConsequence()
        {
            //To update !
            sceneLoader.ReloadScene();
        }

        IEnumerator PlayerDelay()
        {
            if (playerControllerIso.isBeingRepulsed)
            {
                playerReallyFell = false;
            }
            yield return new WaitForSeconds(timePlayerHasToReact);
            playerReallyFell = true;
        }
    }
}