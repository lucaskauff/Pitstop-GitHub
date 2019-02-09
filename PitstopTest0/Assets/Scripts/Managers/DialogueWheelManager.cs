using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueWheelManager : UIManager
{
    public GameObject[] dialogueWheelSlotsV1;

    void Update()
    {
        //Show dialogue wheel slots
        if (inputManager.displayDialogueWheelKey)
        {
            for (int i = 0; i < dialogueWheelSlotsV1.Length; i++)
            {
                dialogueWheelSlotsV1[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < dialogueWheelSlotsV1.Length; i++)
            {
                dialogueWheelSlotsV1[i].SetActive(false);
            }
        }
    }
}