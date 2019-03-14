using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //Menu keys
    public bool escKey;
    public bool pauseKey;

    //UI keys
    public bool displayDialogueWheelKey;

    //Mouse inputs
    public Vector2 cursorPosition;

    //Player keys
    public float horizontalInput;
    public float verticalInput;
    public bool dashKey;

    //Crystal keys
    public bool scanKey;
    public bool shootKey;

    void Update()
    {
        escKey = Input.GetKeyDown(KeyCode.Escape);
        pauseKey = Input.GetKeyDown(KeyCode.P);

        displayDialogueWheelKey = Input.GetKey(KeyCode.Space);

        cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        dashKey = Input.GetKeyDown(KeyCode.LeftShift);

        scanKey = Input.GetKey("mouse 1");
        shootKey = Input.GetKeyDown("mouse 0");
    }
}