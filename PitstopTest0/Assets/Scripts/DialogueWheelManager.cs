using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueWheelManager : MonoBehaviour
{
    public GameObject[] dialogueWheelSlotsV1;
    public LayerMask layerRaycast;

    private void Update()
    {
        /*
        Vector2 playerPos = transform.position;
        Vector2 cursorDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        RaycastHit2D hit = Physics2D.Raycast(playerPos, cursorDirection, 10, layerRaycast);
        Debug.DrawRay(playerPos, cursorDirection, Color.red);
        */

        //Show dialogue wheel slots on space maintained pressed
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