using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueWheelManagerV1 : MonoBehaviour
{
    public GameObject[] dialogueWheelSlotsV1;
    InputManager inputManager;
    UIManager uIManager;

    private void Start()
    {
        inputManager = GameManager.Instance.inputManager;
        uIManager = GameManager.Instance.uIManager;
    }

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

    public void SendNativeTo()
    {

    }
}