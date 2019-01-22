using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueWheelManager : MonoBehaviour
{
    public GameObject[] dialogueWheelSlotsV1;

    private void Update()
    {
        Vector2 playerPos = transform.position;
        Vector2 wheelDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Vector2 wheelOrigin = playerPos + wheelDirection.normalized;

        RaycastHit2D hit = Physics2D.Raycast(wheelOrigin, wheelDirection, 10);
        Debug.DrawRay(wheelOrigin, wheelDirection, Color.red);
        
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