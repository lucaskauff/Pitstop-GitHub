using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Pitstop
{
    public class DialogueManager : MonoBehaviour
    {
        //GameManager
        InputManager inputManager;

        //Public
        public bool readyToDisplay = false;
        public bool playerReading = false;
        public bool isCurrentSentenceFinished = false;

        //Serializable
        [SerializeField]
        float currentLetterSpeed = 0;
        [SerializeField]
        float originalLetterSpeed = 0;
        [SerializeField]
        Animator diaBox = default;
        
        public GameObject nameText = default;
        
        public GameObject dialogueText = default;

        public bool interactionDebug = false;

        [SerializeField]
        PlayerControllerIso playerController = default;

        //Private
        Queue<string> sentences;

        void Start()
        {
            inputManager = GameManager.Instance.inputManager;

            sentences = new Queue<string>();

            readyToDisplay = true;

            ResetLetterSpeed();
        }

        private void Update()
        {
            if (playerReading && inputManager.skipActualDialogueBox)
            {
                if (isCurrentSentenceFinished)
                {
                    DisplayNextSentence();
                    return;
                }
                else if (!interactionDebug)
                {
                    currentLetterSpeed = 0f;
                }
                else
                {
                    interactionDebug = false;
                }
            }
        }

        public void StartDialogue(Dialogue dialogue)
        {
            //questionnable
            playerController.canMove = false;

            playerReading = true;

            nameText.GetComponent<TextMeshProUGUI>().text = dialogue.name;

            sentences.Clear();

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }

            //dialogueBox apparition
            DialogueBoxPopIn();

            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            string sentence = sentences.Dequeue();

            //Displays letter by letter
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }

        IEnumerator TypeSentence(string sentence)
        {
            isCurrentSentenceFinished = false;

            dialogueText.GetComponent<TextMeshProUGUI>().text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                dialogueText.GetComponent<TextMeshProUGUI>().text += letter;
                yield return new WaitForSeconds(currentLetterSpeed);
            }

            isCurrentSentenceFinished = true;
            ResetLetterSpeed();
        }

        public void EndDialogue()
        {
            //dialogueBox disapparition
            DialogueBoxPopOut();

            playerController.canMove = true;
            playerReading = false;
        }

        void DialogueBoxPopIn()
        {
            diaBox.SetTrigger("PopIn");
        }

        void DialogueBoxPopOut()
        {
            diaBox.SetTrigger("PopOut");
        }

        void ResetLetterSpeed()
        {
            currentLetterSpeed = originalLetterSpeed;
        }
    }
}