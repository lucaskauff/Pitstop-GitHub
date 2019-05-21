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

        [Header("Public")]
        public bool readyToDisplay = false;
        public bool playerReading = false;
        public bool isCurrentSentenceFinished = false;
        public string codeOfTheLastTriggeringSentence = null;

        [Header("Serializable")]
        [SerializeField] float currentLetterSpeed = 0;
        [SerializeField] float originalLetterSpeed = 0;
        [SerializeField] Animator diaBox = default;
        [SerializeField] GameObject passDialogueArrow = default;
        [SerializeField] PlayerControllerIso playerController = default;

        [HideInInspector] public GameObject nameText = default;
        [HideInInspector] public GameObject dialogueText = default;
        [HideInInspector] public bool interactionDebug = false;

        //Private
        Queue<string> namesOfSpeakers;
        Queue<string> sentences;

        bool willtheCurrentSentencesTriggeredSomethingWhenFinished = false;
        string memoryForTheTriggerCode = null;

        void Start()
        {
            inputManager = GameManager.Instance.inputManager;

            namesOfSpeakers = new Queue<string>();
            sentences = new Queue<string>();

            readyToDisplay = true;

            ResetLetterSpeed();
        }

        private void Update()
        {
            if (isCurrentSentenceFinished)
            {
                passDialogueArrow.SetActive(true);
            }
            else
            {
                passDialogueArrow.SetActive(false);
            }

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

        public void StartDialogue(Dialogue dialogue, bool triggerOrNot, string codeForTrigger)
        {
            //questionnable
            playerController.canMove = false;

            playerReading = true;

            //nameText.GetComponent<TextMeshProUGUI>().text = dialogue.name;

            namesOfSpeakers.Clear();
            sentences.Clear();

            willtheCurrentSentencesTriggeredSomethingWhenFinished = triggerOrNot;

            if (triggerOrNot) memoryForTheTriggerCode = codeForTrigger;
            else memoryForTheTriggerCode = null;

            
            /*
            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
            */

            foreach (SpecificDialoguePart dialoguePart in dialogue.allDialoguePart)
            {
                namesOfSpeakers.Enqueue(dialoguePart.speaker);
                sentences.Enqueue(dialoguePart.sentence);
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
                
                codeOfTheLastTriggeringSentence = memoryForTheTriggerCode;

                return;
            }

            string nameOfTheCurrentSpeaker = namesOfSpeakers.Dequeue();
            string sentence = sentences.Dequeue();

            //Displays letter by letter
            StopAllCoroutines();
            nameText.GetComponent<TextMeshProUGUI>().text = nameOfTheCurrentSpeaker;
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