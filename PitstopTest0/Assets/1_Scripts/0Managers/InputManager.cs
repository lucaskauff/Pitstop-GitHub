using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class InputManager : MonoBehaviour
    {
        GameManager gameManager;

        //General
        public bool anyKeyPressed;

        //Menu keys
        public bool escKey;

        //UI keys
        public bool displayDialogueWheelKey;
        public bool skipActualDialogueBox;

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

        private void Start()
        {
            gameManager = GameManager.Instance;
        }

        void Update()
        {
            anyKeyPressed = Input.anyKeyDown;
            //
            escKey = Input.GetKeyDown(KeyCode.Escape);
            //
            displayDialogueWheelKey = Input.GetKey(KeyCode.Space);
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
            {
                skipActualDialogueBox = true;
            }
            else
            {
                skipActualDialogueBox = false;
            }
            //
            interactionButton = Input.GetKeyDown(KeyCode.E);
            //
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
            //ATTENTION ATTENTION CLASS : this led to some bugs
            /*
            if (gameManager.languageSetToEnglish == false)
            {
                horizontalInput = Input.GetAxisRaw("Horizontal (French)");
                verticalInput = Input.GetAxisRaw("Vertical (French)");
            }
            else
            {
                horizontalInput = Input.GetAxisRaw("Horizontal (English)");
                verticalInput = Input.GetAxisRaw("Vertical (English)");
            }
            */
            //
            dashKey = Input.GetKeyDown(KeyCode.LeftShift);
            //
            cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            onLeftClick = Input.GetKeyDown(KeyCode.Mouse0);
            leftClickBeingPressed = Input.GetKey(KeyCode.Mouse0);
            onLeftClickReleased = Input.GetKeyUp(KeyCode.Mouse0);
            onRightClick = Input.GetKeyDown(KeyCode.Mouse1);
            rightClickBeingPressed = Input.GetKey(KeyCode.Mouse1);
            onRightClickReleased = Input.GetKeyUp(KeyCode.Mouse1);
        }
    }
}