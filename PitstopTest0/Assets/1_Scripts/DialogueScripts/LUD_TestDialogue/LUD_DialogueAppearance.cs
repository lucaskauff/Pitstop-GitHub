﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pitstop
{
    public class LUD_DialogueAppearance : MonoBehaviour
    {
        
        [SerializeField]
        GameObject dialogueSpace = default;
        public float delay = 7f;
        float timer = 0f;

        public bool isDialogueSpaceActive = false;

        // Start is called before the first frame update
        void Start()
        {
            timer = 0f;
            dialogueSpace.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (isDialogueSpaceActive)
            {
                if (timer >= delay)
                {
                    dialogueSpace.SetActive(false);
                    //timer = 0f;
                    isDialogueSpaceActive = false;

                }

                timer += Time.deltaTime;
            }
        }


        public void ReactionAppearance(Sprite sign1, Sprite sign2, Sprite sign3, bool wasExclamationTriggered)
        {
            /*
            //Faire apparaitre pendant un temps la bulle
            if (!isDialogueSpaceActive)
            {
                isDialogueSpaceActive = true;
                dialogueSpace.SetActive(true);

            }

            //dialogueSpace.GetComponentInChildren<Text>().text = wrotenSentence;
            dialogueSpace.transform.Find("AnswerWord1").GetComponent<Image>().sprite = sign1;
            dialogueSpace.transform.Find("AnswerWord2").GetComponent<Image>().sprite = sign2;
            dialogueSpace.transform.Find("AnswerWord3").GetComponent<Image>().sprite = sign3;

            */
            StartCoroutine(ReactionAppearWithDelay(sign1, sign2, sign3, wasExclamationTriggered));


            timer = 0f;
        }

        IEnumerator ReactionAppearWithDelay(Sprite _sign1, Sprite _sign2, Sprite _sign3, bool _wasExclamationTriggered)
        {
            if (_wasExclamationTriggered)
            {
                yield return new WaitForSeconds(FindObjectOfType<LUD_NativeHeartheSentence>().delayBeforeExclamationDisapperance);
            }
            else
            {
                yield return null;
            }

            if (!isDialogueSpaceActive)
            {
                isDialogueSpaceActive = true;
                dialogueSpace.SetActive(true);

            }

            //dialogueSpace.GetComponentInChildren<Text>().text = wrotenSentence;
            dialogueSpace.transform.Find("AnswerWord1").GetComponent<Image>().sprite = _sign1;
            dialogueSpace.transform.Find("AnswerWord2").GetComponent<Image>().sprite = _sign2;
            dialogueSpace.transform.Find("AnswerWord3").GetComponent<Image>().sprite = _sign3;

            //timer = 0f;
        }
    }
}