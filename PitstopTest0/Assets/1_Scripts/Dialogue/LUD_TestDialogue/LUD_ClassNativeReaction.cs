using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pitstop
{
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

            willTriggeredExclamation = (data[2] == "true" || data[2] == "TRUE");
            //Debug.Log("willTriggeredExclamation = " + (data[2] == "true"));

            codeForReaction = data[3];

            answerWord1 = StringToImage(data[4]);

            answerWord2 = StringToImage(data[5]);

            //Debug.Log("data[5] = " + data[5]);
            //Debug.Log("data[6] = " + data[6]);
            answerWord3 = StringToImage(data[6]);



        }

        private Sprite StringToImage(string code)
        {
            if (code == "?")
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/CrappySigns(by_Luc)/interrogation_square");
            }
            else if (code == "north")
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/Main_Nord");
            }
            else if (code == "south")
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/Main_Sud");
            }
            else if (code == "east")
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/Main_Est");
            }
            else if (code == "west")
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/Main_Ouest");
            }
            else if (code == "me")
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/CrappySigns(by_Luc)/me_square");
            }
            else if (code == "you")
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/CrappySigns(by_Luc)/you_square");
            }
            else if (code == "see")
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/CrappySigns(by_Luc)/see_square");
            }
            else if (code == "photo")
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/CrappySigns(by_Luc)/photo_square");
            }
            else if (code == "gniack")
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/CrappySigns(by_Luc)/gniack_square");
            }
            else if (code == "no")
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/CrappySigns(by_Luc)/no_square");
            }
            else if (code == "repair")
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/CrappySigns(by_Luc)/repair_square");
            }
            else if (code == "danger")
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/CrappySigns(by_Luc)/danger_square");
            }
            else if (code == "child")
            {
                Debug.Log("Child as been tested");
                return Resources.Load<Sprite>("LUD_Sprites_Word/CrappySigns(by_Luc)/child_square");
            }


            else if (code == "Beet")
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/Old/BeetSprite");
            }
            else if (code == "Mushroom")
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/Old/MushroomSprite");
            }
            else if (code == "Apple")
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/Old/AppleSprite");
            }
            else
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/emptySprite");
            }
        }

    }
}