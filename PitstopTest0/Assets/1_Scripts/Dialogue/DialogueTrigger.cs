using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField]
        bool onlyActivatableOnce = false;
        [SerializeField]
        bool triggerDialogueOnStart = false;
        [SerializeField]
        DialogueManager dialogueManager;
        [SerializeField]
        Dialogue dialogue;

        bool activationCheck = false;

        private void Update()
        {
            if (triggerDialogueOnStart && !activationCheck && dialogueManager.readyToDisplay)
            {
                TriggerDialogue();

                activationCheck = true;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.name == "Zayn" && !dialogueManager.playerReading && !activationCheck)
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
            dialogueManager.StartDialogue(dialogue);
        }
    }
}