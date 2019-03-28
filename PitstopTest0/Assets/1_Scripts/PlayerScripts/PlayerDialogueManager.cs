using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class PlayerDialogueManager : MonoBehaviour
    {
        //SerializedField
        [SerializeField]
        float raycastDistance = 10;

        public bool spaceHold = false;

        private void Update()
        {
            Vector2 playerPos = transform.position;
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            Vector2 raycastOrigin = playerPos + cursorPos.normalized;

            if (Input.GetKey("space"))
            {
                spaceHold = true;
            }
            else
            {
                spaceHold = false;
            }
        }
    }
}