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
                NativeAnswer("BETTERAVE !!");
            }

            else if (sumOfSentence == 9 || sumOfSentence == 27)
            {
                NativeAnswer("POMME !!");
            }

            else if (sumOfSentence == 15 || sumOfSentence == 11)
            {
                NativeAnswer("CHAMPIGNON !!");
            }

            else
            {
                NativeAnswer("Il n'y a rien !");
            }
        }
        
    }

    void NativeAnswer(string answer)
    {
        Debug.Log("Le natif répond : " + answer);
    }

    int ValueOfTheSentence (List<int> listOfInt)
    {
        int sum = 1;

        foreach (int item in listOfInt)
        {
            sum = sum * item;
        }

        Debug.Log("sum = " + sum.ToString());

        return sum;
    }

}
