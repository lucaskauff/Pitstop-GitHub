using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class DialogueTrigger : MonoBehaviour
    {
        //GameManager
        GameManager gameManager;
        
        //Serializable
        [SerializeField]
        bool onlyActivatableOnce = false;
        [SerializeField]
        bool triggerDialogueOnStart = false;
        [SerializeField]
        DialogueManager dialogueManager = default;
        [SerializeField]
        Dialogue dialogueInEnglish = default;
        [SerializeField]
        Dialogue dialogueInFrench = default;

        //Private
        Dialogue activeDialogue;
        bool activationCheck = false;

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
                TriggerDialogue();

                activationCheck = true;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                TriggerDialogue();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                TriggerDialogue();
            }
        }

        public void TriggerDialogue()
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
    }
}