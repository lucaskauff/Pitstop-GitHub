using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LUD_TestButton : MonoBehaviour
{
    //SerializeField
    [SerializeField]
    int valueOfTheWord = 0;

    //Private
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WhenClicked ()
    {
        Debug.Log("Click");
        //FindObjectOfType<LUD_DialogueManagement>().AddAWordToTheSentence(valueOfTheWord);
    }
}
