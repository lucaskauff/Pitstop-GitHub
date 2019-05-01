using System.Collections;
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
        public bool isDialogueWheelActive = false;



        [Header("Sentence Parameters")]

        [SerializeField]
        private List<int> sentence = new List<int>();
        [SerializeField]
        private List<Image> spriteSentenceUI = new List<Image>();

        [Header("Natives Parameters")]
        
        public GameObject[] nativesList;



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
            isDialogueWheelActive = true;
        }

        public void WheelDisappearance()
        {
            dialogueWheel.SetActive(false);
            isDialogueWheelActive = false;

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
                foreach (GameObject element in nativesList)
                {
                    element.GetComponent<LUD_NativeHeartheSentence>().HearASentence(sentence);
                }

                //nativesList[0].GetComponent<LUD_NativeHeartheSentence>().HearASentence(sentence);
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
                return Resources.Load<Sprite>("LUD_Sprites_Word/Big_Square_Sprites/me_square_big");
            }

            else if (value == 19)
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/Big_Square_Sprites/you_square_big");
            }
            else if (value == 23)
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/Big_Square_Sprites/see_square_big");
            }
            else if (value == 29)
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/Big_Square_Sprites/photo_square_big");
            }
            else if (value == 31)
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/Big_Square_Sprites/gniack_square_big");
            }
            else if (value == 37)
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/Big_Square_Sprites/no_square_big");
            }
            else if (value == 41)
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/CrappySigns(by_Luc)/repair_square");
            }
            else if (value == 43)
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/Big_Square_Sprites/danger_square_big");
            }
            else if (value == 47)
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/CrappySigns(by_Luc)/child_square");
            }



            else
            {
                return Resources.Load<Sprite>("LUD_Sprites_Word/Big_Square_Sprites/interrogation_square_big");
            }
        }
    }
}