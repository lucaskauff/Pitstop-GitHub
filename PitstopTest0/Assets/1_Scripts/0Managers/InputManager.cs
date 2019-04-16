using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class InputManager : MonoBehaviour
    {
        //General
        public bool anyKeyPressed;

        //Menu keys
        public bool escKey;

        //UI keys
        public bool displayDialogueWheelKey;

        //Interaction button
        public bool interactionButton;

        //Player keys
        public float horizontalInput;
        public float verticalInput;
        public bool dashKey;

        //Mouse inputs
        public Vector2 cursorPosition;
        public bool onLeftClick;
        public bool leftClickBeingPressed;
        public bool onLeftClickReleased;
        public bool onRightClick;
        public bool rightClickBeingPressed;
        public bool onRightClickReleased;

        void Update()
        {
            anyKeyPressed = Input.anyKeyDown;
            //
            escKey = Input.GetKeyDown(KeyCode.Escape);
            //
            displayDialogueWheelKey = Input.GetKey(KeyCode.Space);
            //
            interactionButton = Input.GetKeyDown(KeyCode.E);
            //
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
            //
            dashKey = Input.GetKeyDown(KeyCode.LeftShift);
            //
            cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            onLeftClick = Input.GetKeyDown("mouse 0");
            leftClickBeingPressed = Input.GetKey("mouse 0");
            onLeftClickReleased = Input.GetKeyUp("mouse 0");
            onRightClick = Input.GetKeyDown("mouse 1");
            rightClickBeingPressed = Input.GetKey("mouse 1");
            onRightClickReleased = Input.GetKeyUp("mouse 1");
        }
    }
}