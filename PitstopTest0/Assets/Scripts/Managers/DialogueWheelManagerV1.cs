using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueWheelManagerV1 : MonoBehaviour
{
    InputManager inputManager;

    public UIManager uIManager;
    //public GameObject[] dialogueWheelSlotsV1;
    public List<GameObject> dialogueWheelSlots;

    private void Start()
    {
        inputManager = GameManager.Instance.inputManager;

        foreach (Transform child in transform)
        {
            dialogueWheelSlots.Add(child.gameObject);
        }
    }

    void Update()
    {
        //Show dialogue wheel slots
        if (inputManager.displayDialogueWheelKey)
        {
            for (int i = 0; i < dialogueWheelSlots.Count; i++)
            {
                dialogueWheelSlots[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < dialogueWheelSlots.Count; i++)
            {
                dialogueWheelSlots[i].SetActive(false);
            }
        }

        /*
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
        */
    }
}