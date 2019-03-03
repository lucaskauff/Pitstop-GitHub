using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LUD_NativeHeartheSentence : MonoBehaviour
{

    //SerializeField

    //Private

    //Public
    public bool isCaptivated = true;


    private void Awake()
    {
        isCaptivated = false;
        
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
                

                //Debug.Log("Index = " + index);

                if (index > FindObjectOfType<LUD_CsvToDataConvertor>().nativeReactionList.Count-1)    //s'il ne trouve rien aucun match
                {
                    currentTestReaction = FindObjectOfType<LUD_CsvToDataConvertor>().nativeReactionList[0];     //il prend la première réponse qui est un "? . ."
                    NativeAnswer(currentTestReaction.answerWord1, currentTestReaction.answerWord2, currentTestReaction.answerWord3);
                    answerFound = true;
                    //Debug.Log("Answer found");
                }

                else
                {
                    currentTestReaction = FindObjectOfType<LUD_CsvToDataConvertor>().nativeReactionList[index];

                    if (currentTestReaction.valueOfPlayerSentence == sumOfSentence)
                    {
                        NativeAnswer(currentTestReaction.answerWord1, currentTestReaction.answerWord2, currentTestReaction.answerWord3);
                        answerFound = true;
                        //Debug.Log("Answer found");

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

        //Debug.Log("sum = " + sum.ToString());

        return sum;
    }


    
}
