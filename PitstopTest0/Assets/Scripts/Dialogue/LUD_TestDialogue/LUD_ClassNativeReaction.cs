using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NativeReaction
{

    public int valueOfPlayerSentence;

    public Sprite answerWord1;
    public Sprite answerWord2;
    public Sprite answerWord3;


    public NativeReaction(string rowToConvert)
    {
        string[] data = rowToConvert.Split(';');

        valueOfPlayerSentence = int.Parse(data[0]);

        answerWord1 = StringToImage(data[1]);

        answerWord2 = StringToImage(data[2]);

        answerWord3 = StringToImage(data[3]);



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
