using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class DialogueTrigger : MonoBehaviour
    {
        InputManager inputManager;

        public DialogueManager dialogueManager;
        public Dialogue dialogue;

        bool playerReading = false;

        private void Start()
        {
            inputManager = GameManager.Instance.inputManager;
        }

        private void Update()
        {
            if (playerReading && inputManager.anyKeyPressed && inputManager.horizontalInput == 0 && inputManager.verticalInput == 0)
            {
                dialogueManager.DisplayNextSentence();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Zayn" && playerReading == false)
            {
                playerReading = true;
                TriggerDialogue();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Zayn" && playerReading == true)
            {
                playerReading = false;
            }
        }

        public void TriggerDialogue()
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
    }
}