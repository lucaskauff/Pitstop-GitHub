﻿using System.Collections;
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

        //Serializable
        [SerializeField]
        float letterSpeed = 0;
        [SerializeField]
        Animator diaBox = default;
        [SerializeField]
        GameObject nameText = default;
        [SerializeField]
        GameObject dialogueText = default;
        [SerializeField]
        PlayerControllerIso playerController = default;

        //Private
        Queue<string> sentences;

        void Start()
        {
            inputManager = GameManager.Instance.inputManager;

            sentences = new Queue<string>();

            readyToDisplay = true;
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