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
        public bool pauseKey;

        //UI keys
        public bool displayDialogueWheelKey;

        //Player keys
        public float horizontalInput;
        public float verticalInput;
        public bool dashKey;

        //Mouse inputs
        public Vector2 cursorPosition;
        public bool onLeftClick;
        public bool leftClickBeingPressed;
        public bool onRightClick;
        public bool rightClickBeingPressed;

        void Update()
        {
            anyKeyPressed = Input.anyKeyDown;

            escKey = Input.GetKeyDown(KeyCode.Escape);
            pauseKey = Input.GetKeyDown(KeyCode.P);

            displayDialogueWheelKey = Input.GetKey(KeyCode.Space);

            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            dashKey = Input.GetKeyDown(KeyCode.LeftShift);

            cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            onLeftClick = Input.GetKeyDown("mouse 0");
            leftClickBeingPressed = Input.GetKey("mouse 0");
            onRightClick = Input.GetKeyDown("mouse 1");
            rightClickBeingPressed = Input.GetKey("mouse 1");
        }
    }
}