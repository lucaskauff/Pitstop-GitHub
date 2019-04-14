﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pitstop
{
    public class LUD_DialogueManagement : MonoBehaviour
    {
        //SerializeField
        [SerializeField]
        GameObject dialogueWheel = default;
        //[SerializeField] Image[] displayedSentence = default;



        [Header("Sentence Parameters")]

        [SerializeField]
        private List<int> sentence = new List<int>();
        [SerializeField]
        private List<Image> spriteSentenceUI = new List<Image>();

        [Header("Natives Parameters")]

        [SerializeField]
        GameObject[] nativesList = default;



        //Private



        void Start()
        {

            sentence.Clear();
        }


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                WheelAppearance();
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                WheelDisappearance();
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                DeleteLastWord();
            }

        }


        public void WheelAppearance()
        {
            dialogueWheel.SetActive(true);
        }

        public void WheelDisappearance()
        {
            dialogueWheel.SetActive(false);

            SentenceIsPrononced();


        }

        public void AddAWordToTheSentence(int valueOfTheWord)
        {
            if (sentence.Count < 3)
            {
                sentence.Add(valueOfTheWord);
                //Debug.Log("word = " + valueOfTheWord.ToString());
            }
            else
            {
                sentence[2] = valueOfTheWord;
            }

            Sprite spriteSelected = IntToSprite(valueOfTheWord);

            spriteSentenceUI[sentence.Count - 1].sprite = spriteSelected;

        }

        public void DeleteLastWord()
        {
            if (sentence.Count > 0)
            {

                spriteSentenceUI[sentence.Count - 1].sprite = Resources.Load<Sprite>("LUD_Sprites_Word/emptySprite");

                sentence.RemoveAt(sentence.Count - 1);
                //Debug.Log("Word remove");

            }

        }

        void SentenceIsPrononced()
        {
            if (sentence.Count != 0)
            {
                nativesList[0].GetComponent<LUD_NativeHeartheSentence>().HearASentence(sentence);
            }


            sentence.Clear();

            foreach (Image image in spriteSentenceUI)
            {
                image.sprite = Resources.Load<Sprite>("LUD_Sprites_Word/emptySprite");
            }
        }




        private Sprite IntToSprite(int value)
        {
            if (value == 5)
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/Main_Nord");
            }

            else if (value == 7)
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/Main_Est");
            }

            else if (value == 13)
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/Main_Sud");
            }

            else if (value == 11)
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/Main_Ouest");
            }

            else if (value == 17)
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/CrappySigns(by_Luc)/me_square");
            }

            else if (value == 19)
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/CrappySigns(by_Luc)/you_square");
            }
            else if (value == 23)
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/CrappySigns(by_Luc)/see_square");
            }
            else if (value == 29)
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/CrappySigns(by_Luc)/photo_square");
            }
            else if (value == 31)
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/CrappySigns(by_Luc)/gniack_square");
            }
            else if (value == 37)
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/CrappySigns(by_Luc)/no_square");
            }
            else if (value == 41)
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/CrappySigns(by_Luc)/repair_square");
            }
            else if (value == 43)
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/CrappySigns(by_Luc)/danger_square");
            }
            else if (value == 47)
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/CrappySigns(by_Luc)/child_square");
            }



            else
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/CrappySigns(by_Luc)/interrogation_square");
            }
        }
    }
}