using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LUD_NativeHeartheSentence : MonoBehaviour
{

    //SerializeField
    [SerializeField]
    bool isCaptivated = true;



    //Private

    //Public

    public void HearASentence(List<int> heardSentence)
    {
        if (isCaptivated)
        {
            int sumOfSentence = ValueOfTheSentence(heardSentence);

            

            if (sumOfSentence == 3)
            {
                NativeAnswer("BEET !!");
            }

            else if (sumOfSentence == 9 || sumOfSentence == 27)
            {
                NativeAnswer("APPLE !!");
            }

            else if (sumOfSentence == 15 || sumOfSentence == 11)
            {
                NativeAnswer("MUSHROOMS !!");
            }

            else
            {
                NativeAnswer("There is nothing !");
            }
        }
        
    }

    void NativeAnswer(string answer)
    {
        //Debug.Log("Le natif répond : " + answer);
        GetComponent<LUD_DialogueAppearance>().ReactionAppearance(answer);
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
