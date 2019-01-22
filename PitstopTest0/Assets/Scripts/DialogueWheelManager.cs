using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueWheelManager : MonoBehaviour
{
    public GameObject[] dialogueWheelSlotsV1;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
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