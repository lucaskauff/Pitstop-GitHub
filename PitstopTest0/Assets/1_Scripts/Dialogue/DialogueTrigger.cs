using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class DialogueTrigger : MonoBehaviour
    {
        GameManager gameManager;
        
        [SerializeField]
        bool onlyActivatableOnce = false;
        [SerializeField]
        bool triggerDialogueOnStart = false;
        [SerializeField]
        DialogueManager dialogueManager;
        [SerializeField]
        Dialogue dialogueInEnglish;
        [SerializeField]
        Dialogue dialogueInFrench;

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
            if (collision.gameObject.tag == "Player" && !dialogueManager.playerReading && !activationCheck)
            {
                TriggerDialogue();

                if (onlyActivatableOnce)
                {
                    activationCheck = true;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player" && !dialogueManager.playerReading && !activationCheck)
            {
                TriggerDialogue();

                if (onlyActivatableOnce)
                {
                    activationCheck = true;
                }
            }
        }

        public void TriggerDialogue()
        {
            dialogueManager.StartDialogue(activeDialogue);
        }

        public void ChangeActiveDialogue()
        {

        }
    }
}