using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NativeReaction
{
    public string testOperation;    
    public int valueOfPlayerSentence;

    public bool willTriggeredExclamation; //pas assigné
    public string codeForReaction;  //pas assigné

    public Sprite answerWord1;
    public Sprite answerWord2;
    public Sprite answerWord3;


    public NativeReaction(string rowToConvert)
    {
        string[] data = rowToConvert.Split(';');
        

        testOperation = data[0];

        valueOfPlayerSentence = int.Parse(data[1]);

        willTriggeredExclamation = (data[2] == "true");
        //Debug.Log("willTriggeredExclamation = " + (data[2] == "true"));

        codeForReaction = data[3];

        answerWord1 = StringToImage(data[4]);

        answerWord2 = StringToImage(data[5]);

        answerWord3 = StringToImage(data[6]);


    }

    private Sprite StringToImage(string code)
    {
        if (code == "?")
        {
            return Resources.Load<Sprite>("LUD_Sprites_Word/interrogationSprite");
        }
        else if (code=="Beet")
        {
            return Resources.Load<Sprite>("LUD_Sprites_Word/BeetSprite");
        }
        else if (code == "Mushroom")
        {
            return Resources.Load<Sprite>("LUD_Sprites_Word/MushroomSprite");
        }
        else if (code == "Apple")
        {
            return Resources.Load<Sprite>("LUD_Sprites_Word/AppleSprite");
        }
        else
        {
            return Resources.Load<Sprite>("LUD_Sprites_Word/emptySprite");
        }
    }

}
