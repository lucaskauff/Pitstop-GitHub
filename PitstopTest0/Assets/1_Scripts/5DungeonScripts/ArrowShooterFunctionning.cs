using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class ArrowShooterFunctionning : MonoBehaviour
    {
        InputManager inputManager;

        [SerializeField] Animator myAnim = default;

        [SerializeField] GameObject interactionButton = default;
        [SerializeField] GameObject whatToShoot = default;
        [SerializeField] Transform fromWhereToShoot = default;
        [SerializeField] float interval = 1;

        bool triggerOnceCheck = false;
        bool goShoot = true;
        GameObject cloneProj;

        private void Start()
        {
            inputManager = GameManager.Instance.inputManager;
        }

        private void Update()
        {
            if (!triggerOnceCheck)
            {
                if (interactionButton.activeInHierarchy && inputManager.interactionButton)
                {
                    myAnim.SetTrigger("TurnOn");
                    interactionButton.SetActive(false);
                    triggerOnceCheck = true;
                }
            }
            else if (goShoot)
            {
                StopCoroutine(WaitForInterval());
                cloneProj = Instantiate(whatToShoot, fromWhereToShoot.position, whatToShoot.transform.rotation);
                cloneProj.GetComponent<ArrowBehaviour>().isFired = true;
                StartCoroutine(WaitForInterval());
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player" && !triggerOnceCheck)
            {
                interactionButton.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                interactionButton.SetActive(false);
            }
        }

        IEnumerator WaitForInterval()
        {
            goShoot = false;
            yield return new WaitForSeconds(interval);
            goShoot = true;
        }
    }
}