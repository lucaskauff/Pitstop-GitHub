using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Pitstop
{
    public class DialogueManager : MonoBehaviour
    {
        InputManager inputManager;

        [SerializeField]
        float letterSpeed = 0;
        [SerializeField]
        float positionOut;
        [SerializeField]
        float popInSpeed;
        [SerializeField]
        float popOutSpeed;
        [SerializeField]
        Animator diaBox;
        [SerializeField]
        GameObject nameText;
        [SerializeField]
        GameObject dialogueText;
        [SerializeField]
        PlayerControllerIso playerController;

        Queue<string> sentences;
        public bool playerReading = false;

        void Start()
        {
            inputManager = GameManager.Instance.inputManager;

            sentences = new Queue<string>();
        }

        private void Update()
        {
            if (playerReading && inputManager.anyKeyPressed)
            {
                DisplayNextSentence();
            }
        }

        public void StartDialogue(Dialogue dialogue)
        {
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
            dialogueText.GetComponent<TextMeshProUGUI>().text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                dialogueText.GetComponent<TextMeshProUGUI>().text += letter;
                yield return new WaitForSeconds(letterSpeed);
            }
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
    }
}