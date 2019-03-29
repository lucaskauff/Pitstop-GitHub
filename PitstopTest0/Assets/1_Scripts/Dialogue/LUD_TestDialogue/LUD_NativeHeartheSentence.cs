using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class LUD_NativeHeartheSentence : MonoBehaviour
    {

        
        

        
        bool isExclamationPointActive;
        float timerForExclamation;

        public GameObject dialogueSpace;


        public GameObject exclamationPointUI;

        public float delayBeforeExclamationDisapperance = 1f;
        public bool isCaptivated = true;


        private void Awake()
        {
            isCaptivated = false;

            exclamationPointUI.SetActive(false);
            isExclamationPointActive = false;
            timerForExclamation = 0f;



        }

        private void Update()
        {
            if (isExclamationPointActive)
            {
                if (timerForExclamation >= delayBeforeExclamationDisapperance)
                {
                    exclamationPointUI.SetActive(false);
                    isExclamationPointActive = false;
                    timerForExclamation = 0f;
                }
                else
                {
                    timerForExclamation += Time.deltaTime;
                }
            }
        }

        public void HearASentence(List<int> heardSentence)
        {

            if (isCaptivated && !GetComponent<LUD_NonDialogueReactions>().isOffended)
            {
                int sumOfSentence = ValueOfTheSentence(heardSentence);

                bool answerFound = false;
                int index = 0;
                NativeReaction currentTestReaction;

                while (answerFound == false)
                {

                    if (index > GetComponent<LUD_CsvToDataConvertor>().nativeReactionList.Count - 1)    //s'il ne trouve aucun match
                    {
                        currentTestReaction = GetComponent<LUD_CsvToDataConvertor>().nativeReactionList[0];     //il prend la première réponse qui est un "? . ."
                        NativeAnswer(currentTestReaction.answerWord1, currentTestReaction.answerWord2, currentTestReaction.answerWord3, false);
                        answerFound = true;
                    }

                    else
                    {
                        currentTestReaction = GetComponent<LUD_CsvToDataConvertor>().nativeReactionList[index];
                        bool isThisReactionTriggered = false;

                        if (currentTestReaction.testOperation == "divide")
                        {
                            if ((sumOfSentence % currentTestReaction.valueOfPlayerSentence) == 0)
                            {
                                isThisReactionTriggered = true;
                            }

                        }
                        else
                        {
                            if (sumOfSentence == currentTestReaction.valueOfPlayerSentence)
                            {
                                isThisReactionTriggered = true;
                            }
                        }

                        if (isThisReactionTriggered)
                        {
                            DisplayingExclamationPoint((currentTestReaction.willTriggeredExclamation));
                            bool isSentenceTriggered = NotDialogueReaction(currentTestReaction.codeForReaction);

                            if (isSentenceTriggered)    //because after Exclamation the sentence can not being triggered
                            {
                                NativeAnswer(currentTestReaction.answerWord1, currentTestReaction.answerWord2, currentTestReaction.answerWord3, currentTestReaction.willTriggeredExclamation);

                            }
                            else
                            {
                                index += 1;
                            }
                            answerFound = true;
                        }
                        else
                        {
                            index += 1;
                        }

                    }

                }
            }
        }

        void NativeAnswer(Sprite word1, Sprite word2, Sprite word3, bool wasExclamationTriggered)
        {

            GetComponent<LUD_DialogueAppearance>().ReactionAppearance(word1, word2, word3, wasExclamationTriggered);
        }

        int ValueOfTheSentence(List<int> listOfInt)
        {
            int sum = 1;

            foreach (int item in listOfInt)
            {
                sum = sum * item;
            }

            return sum;
        }

        void DisplayingExclamationPoint(bool isDisplayed)
        {
            dialogueSpace.SetActive(false);
            GetComponent<LUD_DialogueAppearance>().isDialogueSpaceActive = false;

            if (isDisplayed)
            {
                //display le point d'exclamation
                exclamationPointUI.SetActive(true);
                isExclamationPointActive = true;
            }

        }

        bool NotDialogueReaction(string code)  //renvoie un bool qui dit si oui ou non une phrase est quand même display
        {
            if (code == "go_down")
            {
                GetComponent<LUD_NonDialogueReactions>().GoDown();
                return false;
            }
            else if (code == "repeat")
            {
                GetComponent<LUD_NonDialogueReactions>().Repeat();
                return false;
            }
            else if (code == "show_the_way")
            {
                GetComponent<LUD_NonDialogueReactions>().ShowTheWay();
                return false;
            }
            else if (code == "native_offended")
            {
                GetComponent<LUD_NonDialogueReactions>().StartCoroutine("LaunchTheDesactivcationaAndReturnToNormal"); 
                return true;
            }

            
            else
            {

                return true;
            }
        }


    }
}