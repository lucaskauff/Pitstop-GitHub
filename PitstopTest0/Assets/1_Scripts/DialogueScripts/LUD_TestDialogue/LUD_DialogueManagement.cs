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

        //InputManager inputManager = default;

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

            //inputManager = FindObjectOfType<InputManager>();
        }


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (FindObjectOfType<PlayerControllerIso>().canMove)  WheelAppearance();
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
                //return Resources.Load<Sprite>("LUD_Sprites_Word/Main_Nord");
                return Resources.Load<Sprite>("Celia_Signs_Hands/celia_nord_outline");
            }

            else if (value == 7)
            {
                //return Resources.Load<Sprite>("LUD_Sprites_Word/Main_Est");
                return Resources.Load<Sprite>("Celia_Signs_Hands/celia_east_outline");
            }

            else if (value == 13)
            {
                //return Resources.Load<Sprite>("LUD_Sprites_Word/Main_Sud");
                return Resources.Load<Sprite>("Celia_Signs_Hands/celia_south_outline");
            }

            else if (value == 11)
            {
                //return Resources.Load<Sprite>("LUD_Sprites_Word/Main_Ouest");
                return Resources.Load<Sprite>("Celia_Signs_Hands/celia_west_outline");
            }

            else if (value == 17)
            {
                //return Resources.Load<Sprite>("LUD_Sprites_Word/Big_Square_Sprites/me_square_big");
                return Resources.Load<Sprite>("Celia_Signs/celia_me_with_outline");
            }

            else if (value == 19)
            {
                //return Resources.Load<Sprite>("LUD_Sprites_Word/Big_Square_Sprites/you_square_big");
                return Resources.Load<Sprite>("Celia_Signs/celia_you_with_outline");
            }
            else if (value == 23)
            {
                //return Resources.Load<Sprite>("LUD_Sprites_Word/Big_Square_Sprites/see_square_big");
                return Resources.Load<Sprite>("Celia_Signs/celia_see_with_outline_2");
            }
            else if (value == 29)
            {
                //return Resources.Load<Sprite>("LUD_Sprites_Word/Big_Square_Sprites/photo_square_big");
                return Resources.Load<Sprite>("Celia_Signs/celia_photo_with_outline");
            }
            else if (value == 31)
            {
                //return Resources.Load<Sprite>("LUD_Sprites_Word/Big_Square_Sprites/gniack_square_big");
                return Resources.Load<Sprite>("Celia_Signs/celia_eat_with_outline");
            }
            else if (value == 37)
            {
                //return Resources.Load<Sprite>("LUD_Sprites_Word/Big_Square_Sprites/no_square_big");
                return Resources.Load<Sprite>("Celia_Signs/celia_no_with_outline_intern");
            }
            else if (value == 41)
            {
                //return Resources.Load<Sprite>("LUD_Sprites_Word/CrappySigns(by_Luc)/repair_square");
                return Resources.Load<Sprite>("Celia_Signs/celia_repair_with_outline");
            }
            else if (value == 43)
            {
                //return Resources.Load<Sprite>("LUD_Sprites_Word/Big_Square_Sprites/danger_square_big");
                return Resources.Load<Sprite>("Celia_Signs/celia_danger_with_outline_intern");
            }
            else if (value == 47)
            {
                //return Resources.Load<Sprite>("LUD_Sprites_Word/CrappySigns(by_Luc)/child_square");
                return Resources.Load<Sprite>("Celia_Signs/celia_child_with_outline");
            }



            else
            {
                return Resources.Load<Sprite>("Celia_Signs/celia_interrogation_with_outline");
            }
        }
    }
}