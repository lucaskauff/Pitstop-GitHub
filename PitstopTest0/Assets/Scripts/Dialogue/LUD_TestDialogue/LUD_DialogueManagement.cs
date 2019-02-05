using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LUD_DialogueManagement : MonoBehaviour
{
    //SerializeField
    [SerializeField]
    GameObject dialogueWheel;

    [Header("Sentence Parameters")]

    [SerializeField]
    private List<int> sentence = new List<int>();

    [Header("Natives Parameters")]

    [SerializeField]
    GameObject[] nativesList;

    

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


    void WheelAppearance()
    {
        dialogueWheel.SetActive(true);
    }
    
    void WheelDisappearance()
    {
        dialogueWheel.SetActive(false);

        SentenceIsPrononced();


    }

    public void AddAWordToTheSentence (int valueOfTheWord)
    {
        if (sentence.Count <3)
        {
            sentence.Add(valueOfTheWord);
            //Debug.Log("word = " + valueOfTheWord.ToString());
        }

    }

    public void DeleteLastWord ()
    {
        if (sentence.Count > 0)
        {
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
        //Debug.Log("Sentence Clear");
    }

}
