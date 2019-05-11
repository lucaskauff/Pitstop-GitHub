using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class DialogueTrigger : MonoBehaviour
    {
        //GameManager
        GameManager gameManager;
        InputManager inputManager;

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
        public bool activationCheck = false;
        public bool debugging = false;

        [Header("Will Something Happen after")]
        public bool triggerSomethingWhenFinished = false;
        public string codeOfTriggeredAction = null;

        private void Start()
        {
            gameManager = GameManager.Instance;
            inputManager = GameManager.Instance.inputManager;
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

            if (interactionButtonNeeded)
            {
                if (debugging)
                {
                    interactionButton.SetActive(true);

                    if (inputManager.interactionButton)
                    {
                        dialogueManager.interactionDebug = true;
                        TriggerDialogueDirectly();
                        debugging = false;
                        return;
                    }
                }
                else
                {
                    interactionButton.SetActive(false);
                }
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
            if (collision.gameObject.tag == "Player")
            {
                if (!interactionButtonNeeded)
                {
                    TriggerDialogueDirectly();
                }
                else
                {
                    debugging = true;
                }
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player" && !interactionButtonNeeded)
            {
                if (dialogueManager.playerReading)
                {
                    dialogueManager.EndDialogue();
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (interactionButtonNeeded)
                {
                    debugging = false;
                }

                if (dialogueManager.playerReading)
                {
                    dialogueManager.EndDialogue();
                }
            }
        }

        public void TriggerDialogueDirectly()
        {
            if (!dialogueManager.playerReading && !activationCheck)
            {
                dialogueManager.StartDialogue(activeDialogue,triggerSomethingWhenFinished,codeOfTriggeredAction);

                if (onlyActivatableOnce)
                {
                    activationCheck = true;
                }
            }
        }
    }
}