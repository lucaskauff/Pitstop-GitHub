﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LUD_TestExploreWithMouseWheel : MonoBehaviour
{
    //SerializeField
    [SerializeField]
    GameObject dialogueWheel;
    [SerializeField]
    Image[] displayedSentence;



    [Header("Sentence Parameters")]

    [SerializeField]
    private List<int> sentence = new List<int>();
    [SerializeField]
    private List<Image> spriteSentenceUI = new List<Image>();

    [Header("Natives Parameters")]

    [SerializeField]
    GameObject[] nativesList;

    [SerializeField]
    List<Button> dialogueWheelButtons = new List<Button>();
    private int actualIndex = 0;




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

        //Debug.Log("Input (MouseWheel)  = " + Input.GetAxis("Mouse ScrollWheel"));

        float mouseScrollwheelValue = Input.GetAxis("Mouse ScrollWheel");

        if (mouseScrollwheelValue != 0)
        {
            ModifyIndexDialogueWheelSign(mouseScrollwheelValue);
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
        if (value == 3)
        {
            return Resources.Load<Sprite>("LUD_Sprites_Word/Main_Nord");
        }

        else if (value == 5)
        {
            return Resources.Load<Sprite>("LUD_Sprites_Word/Main_Est");
        }

        else if (value == 7)
        {
            return Resources.Load<Sprite>("LUD_Sprites_Word/Main_Sud");
        }

        else if (value == 11)
        {
            return Resources.Load<Sprite>("LUD_Sprites_Word/Main_Ouest");
        }

        else
        {
            return Resources.Load<Sprite>("LUD_Sprites_Word/interrogationSprite");
        }
    }

    private void ModifyIndexDialogueWheelSign (float value)
    {
        int modificator = 0;

        if (value < 0)
        {
            modificator = -1;
        }
       else if (value > 0)
        {
            modificator = 1;
        }
        else
        {
            modificator = 0;
        }

        actualIndex += modificator;

        if (actualIndex <0)
        {
            actualIndex = dialogueWheelButtons.Count - 1;
        }


    }

}
