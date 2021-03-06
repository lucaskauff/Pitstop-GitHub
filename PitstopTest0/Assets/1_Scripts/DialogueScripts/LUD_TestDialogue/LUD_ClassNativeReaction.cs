﻿using System.Collections;
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

        public Sprite StringToImage(string code)
        {
            if (code == "?")
            {
                return Resources.Load<Sprite>("Celia_Signs/celia_interrogation_with_outline");
            }
            else if (code == "north")
            {
                return Resources.Load<Sprite>("Celia_Signs_Hands/celia_nord_outline");
            }
            else if (code == "south")
            {
                //return Resources.Load<Sprite>("LUD_Sprites_Word/Main_Sud");
                return Resources.Load<Sprite>("Celia_Signs_Hands/celia_south_outline");
            }
            else if (code == "east")
            {
                //return Resources.Load<Sprite>("LUD_Sprites_Word/Main_Est");
                return Resources.Load<Sprite>("Celia_Signs_Hands/celia_east_outline");
            }
            else if (code == "west")
            {
                //return Resources.Load<Sprite>("LUD_Sprites_Word/Main_Ouest");
                return Resources.Load<Sprite>("Celia_Signs_Hands/celia_west_outline");
            }
            else if (code == "me")
            {
                //return Resources.Load<Sprite>("LUD_Sprites_Word/Big_Square_Sprites/me_square_big");
                return Resources.Load<Sprite>("Celia_Signs/celia_me_with_outline"); ;
            }
            else if (code == "you")
            {
                //return Resources.Load<Sprite>("LUD_Sprites_Word/Big_Square_Sprites/you_square_big")
                return Resources.Load<Sprite>("Celia_Signs/celia_you_with_outline"); ;
            }
            else if (code == "see")
            {
                //return Resources.Load<Sprite>("LUD_Sprites_Word/Big_Square_Sprites/see_square_big");
                return Resources.Load<Sprite>("Celia_Signs/celia_see_with_outline_2");
            }
            else if (code == "photo")
            {
                //return Resources.Load<Sprite>("LUD_Sprites_Word/Big_Square_Sprites/photo_square_big");
                return Resources.Load<Sprite>("Celia_Signs/celia_photo_with_outline"); ;
            }
            else if (code == "gniack")
            {
                //return Resources.Load<Sprite>("LUD_Sprites_Word/Big_Square_Sprites/gniack_square_big");
                return Resources.Load<Sprite>("Celia_Signs/celia_eat_with_outline"); ;
            }
            else if (code == "no")
            {
                //return Resources.Load<Sprite>("LUD_Sprites_Word/Big_Square_Sprites/no_square_big");
                return Resources.Load<Sprite>("Celia_Signs/celia_no_with_outline_intern"); ;
            }
            else if (code == "repair")
            {
                //return Resources.Load<Sprite>("LUD_Sprites_Word/CrappySigns(by_Luc)/repair_square");
                return Resources.Load<Sprite>("Celia_Signs/celia_repair_with_outline"); ;
            }
            else if (code == "danger")
            {
                //return Resources.Load<Sprite>("LUD_Sprites_Word/Big_Square_Sprites/danger_square_big");
                return Resources.Load<Sprite>("Celia_Signs/celia_danger_with_outline_intern"); ;
            }
            else if (code == "child")
            {
                //return Resources.Load<Sprite>("LUD_Sprites_Word/CrappySigns(by_Luc)/child_square");
                return Resources.Load<Sprite>("Celia_Signs/celia_child_with_outline"); ;
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