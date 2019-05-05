using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class DialogueTrigger : MonoBehaviour
    {
        //GameManager
        GameManager gameManager;

        [Header("Serializable")]
        //Bools must be false by default pls !
        [SerializeField] bool onlyActivatableOnce = false;
        [SerializeField] bool triggerDialogueOnStart = false;
        [SerializeField] bool interactionButtonNeeded = false;
        [SerializeField] DialogueManager dialogueManager = default;
        [SerializeField] GameObject interactionButton = default;
        [SerializeField] Dialogue dialogueInEnglish = default;
        [SerializeField] Dialogue dialogueInFrench = default;

        //Private
        Dialogue activeDialogue;
        bool activationCheck = false;
        bool diaHasBeenTriggeredOnInput = false;

        private void Start()
        {
            gameManager = GameManager.Instance;
        }

        private void Update()
        {
            if (gameManager.languageSetToEnglish)
            {
                activeDialogue = dialogueInEnglish;
            }
            else
            {
                activeDialogue = dialogueInFrench;
            }

            if (triggerDialogueOnStart && !activationCheck && dialogueManager.readyToDisplay)
            {
                TriggerDialogueDirectly();

                activationCheck = true;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player" && !interactionButtonNeeded)
            {
                TriggerDialogueDirectly();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player" && !interactionButtonNeeded)
            {
                TriggerDialogueDirectly();
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player" && interactionButtonNeeded && !diaHasBeenTriggeredOnInput)
            {
                TriggerDialogueOnInput();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player" && interactionButtonNeeded)
            {
                interactionButton.SetActive(false);

                if (!onlyActivatableOnce)
                {
                    diaHasBeenTriggeredOnInput = false;
                }
            }
        }

        public void TriggerDialogueDirectly()
        {
            if (!dialogueManager.playerReading && !activationCheck)
            {
                dialogueManager.StartDialogue(activeDialogue);

                if (onlyActivatableOnce)
                {
                    activationCheck = true;
                }
            }
        }

        public void TriggerDialogueOnInput()
        {
            if (!dialogueManager.playerReading && !activationCheck)
            {
                interactionButton.SetActive(true);

                if (gameManager.inputManager.interactionButton)
                {
                    interactionButton.SetActive(false);
                    dialogueManager.StartDialogue(activeDialogue);

                    if (onlyActivatableOnce)
                    {
                        activationCheck = true;
                    }

                    diaHasBeenTriggeredOnInput = true;
                }
            }            
        }
    }
}