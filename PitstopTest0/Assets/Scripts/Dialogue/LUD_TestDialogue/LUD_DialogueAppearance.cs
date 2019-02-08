using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LUD_DialogueAppearance : MonoBehaviour
{
    //Serializeield
    [SerializeField]
    GameObject dialogueSpace;
    [SerializeField]
    float delay = 7f;


    //Private
    bool isDialogueSpaceActive = false;
    float timer = 0f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        dialogueSpace.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDialogueSpaceActive)
        {
            if (timer>=delay)
            {
                dialogueSpace.SetActive(false);
                timer = 0f;
                isDialogueSpaceActive = false;

            }

            timer += Time.deltaTime;
        }
    }


    public void ReactionAppearance (string wrotenSentence)
    {
        //Faire apparaitre pendant un temps la bulle
        if (!isDialogueSpaceActive)
        {
            isDialogueSpaceActive = true;
            dialogueSpace.SetActive(true);

        }

        dialogueSpace.GetComponentInChildren<Text>().text = wrotenSentence;

    }


}
