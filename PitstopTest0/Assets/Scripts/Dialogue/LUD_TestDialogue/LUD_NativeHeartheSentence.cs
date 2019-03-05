using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LUD_NativeHeartheSentence : MonoBehaviour
{

    //SerializeField
    
    [SerializeField]
    GameObject exclamationPointUI;
    [SerializeField]
    float delayBeforeExclamationDisapperance = 1f;

    //Private
    bool isExclamationPointActive;
    float timerForExclamation;

    //Public
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
        
        if (isCaptivated)
        {
            int sumOfSentence = ValueOfTheSentence(heardSentence);

            bool answerFound = false;
            int index = 0;
            NativeReaction currentTestReaction;

            while (answerFound == false)
            {

                if (index > FindObjectOfType<LUD_CsvToDataConvertor>().nativeReactionList.Count - 1)    //s'il ne trouve aucun match
                {
                    currentTestReaction = FindObjectOfType<LUD_CsvToDataConvertor>().nativeReactionList[0];     //il prend la première réponse qui est un "? . ."
                    NativeAnswer(currentTestReaction.answerWord1, currentTestReaction.answerWord2, currentTestReaction.answerWord3);
                    answerFound = true;
                }

                else
                {
                    currentTestReaction = FindObjectOfType<LUD_CsvToDataConvertor>().nativeReactionList[index];
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

                        if (isSentenceTriggered)
                        {
                            NativeAnswer(currentTestReaction.answerWord1, currentTestReaction.answerWord2, currentTestReaction.answerWord3);
                            
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

    void NativeAnswer(Sprite word1, Sprite word2, Sprite word3)
    {
        
        GetComponent<LUD_DialogueAppearance>().ReactionAppearance(word1,word2,word3);
    }

    int ValueOfTheSentence (List<int> listOfInt)
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
        if (isDisplayed)
        {
            //display le point d'exclamation
            exclamationPointUI.SetActive(true);
            isExclamationPointActive = true;
        }

    }

    bool NotDialogueReaction (string code)  //renvoie un bool qui dit si oui ou non une phrase est quand même display
    {
        if (code == "go_down")
        {
            Debug.Log("Go Down reaction activate");
            return false;
        }
        else
        {
            
            return true;
        }
    }

    
}
