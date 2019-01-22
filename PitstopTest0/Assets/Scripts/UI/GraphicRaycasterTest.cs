using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GraphicRaycasterTest : MonoBehaviour
{
    GraphicRaycaster raycaster;
    PointerEventData pointerEventData;
    PointerEventData playerPointer;
    EventSystem eventSystem;

    public GameObject player;
    private float distancePlayerCursor;

    void Start()
    {
        //Fetch the raycaster from the GameObject (the Canvas)
        raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        eventSystem = GetComponent<EventSystem>();
    }

    void Update()
    {
        /*
        playerPointer = new PointerEventData(eventSystem);
        playerPointer.position = player.transform.position;
        pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        distancePlayerCursor = Vector2.Distance(playerPointer.position, pointerEventData.position);
        Debug.Log(distancePlayerCursor);
        */

        //Check if the left Mouse button is clicked
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //Set up the new Pointer Event
            pointerEventData = new PointerEventData(eventSystem);
            //Set the Pointer Event Position to that of the mouse position
            pointerEventData.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics raycaster and mouse click position
            raycaster.Raycast(pointerEventData, results);

            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                Debug.Log("Hit " + result.gameObject.name);
            }
        }
    }
}