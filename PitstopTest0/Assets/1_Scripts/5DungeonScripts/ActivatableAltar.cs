using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Pitstop
{
    public class ActivatableAltar : MonoBehaviour
    {
        InputManager inputManager;

        [SerializeField] PlayerControllerIso playerControllerIso = default;
        [SerializeField] GameObject interactionButton = default;
        [SerializeField] GameObject mainCam = default;
        [SerializeField] GameObject vCamPlayer = default;
        [SerializeField] GameObject vCamAssociatedDoor = default;
        [SerializeField] Animator associatedDoorAnim = default;
        [SerializeField] CinemachineImpulseSource associatedDoorImpulseSource = default;
        [SerializeField] Transform doorPointer = default;
        [SerializeField] Animator additionnalDoor = default;

        [HideInInspector] public bool isActivatable = false;
        [HideInInspector] public bool doorIsOpened = false;
        bool triggerOnceCheck = false;
        bool triggerDoorOpeningCheck = false;

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
                    vCamAssociatedDoor.SetActive(true);
                    vCamPlayer.SetActive(false);
                    playerControllerIso.canMove = false;
                    interactionButton.SetActive(false);
                    triggerOnceCheck = true;

                    if (additionnalDoor != null)
                    {
                        additionnalDoor.SetTrigger("OpenTheDoor");
                    }
                }
            }
            else if ((Vector2)mainCam.transform.position == (Vector2)doorPointer.position && !triggerDoorOpeningCheck)
            {
                associatedDoorAnim.SetTrigger("OpenTheDoor");
                associatedDoorImpulseSource.GenerateImpulse();
                triggerDoorOpeningCheck = true;
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

        public void ReturnToPlayerCamera()
        {
            vCamPlayer.SetActive(true);
            vCamAssociatedDoor.SetActive(false);
            playerControllerIso.canMove = true;
        }
    }
}