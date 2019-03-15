using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField]
        DialogueManager dialogueManager;
        [SerializeField]
        Dialogue dialogue;
        [SerializeField]
        bool onlyActivatableOnce;

        bool activationCheck = false;

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