using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pitstop
{
    public class LUD_ExploreWheelWithMouseAngle : MonoBehaviour
    {
        
        public Button[] buttons = new Button[12];

        public float angle;
        public Vector2 origin;

        private Camera cam;

        public GameObject currentValidButton = null;

        public Transform dialogueWheelUI;

        private void Awake()
        {
            cam = Camera.main;
        }

        // Start is called before the first frame update
        void Start()
        {
            currentValidButton = buttons[0].gameObject;
            
            //origin = this.transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            origin = cam.ScreenToWorldPoint(dialogueWheelUI.position);

            if (GetComponent<LUD_DialogueManagement>().isDialogueWheelActive)
            {
                SetAngle();

                PickElement();


                if (Input.GetMouseButtonDown(0))
                {
                    CurrentValidButtonIsSelected();
                }
            }
        }

        public void SetAngle()
        {
            
            Vector2 direction = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition) - origin;
            angle = Mathf.Atan2(direction.normalized.y, direction.normalized.x) * Mathf.Rad2Deg;
            angle -= 90 / 4 * 3;

            angle -= 180 / buttons.Length;

            angle = angle >= 0 ? angle : angle + 360;
            angle = angle > 360 ? angle - 360 : angle;
            
        }

        public void PickElement()
        {
            int actualHandledElement;

            actualHandledElement = Mathf.FloorToInt(((angle * buttons.Length) / 360));

            HandleElements(actualHandledElement);
        }

        public void HandleElements(int _index)
        {
            buttons[_index].Select();

            currentValidButton = buttons[_index].gameObject;
        }

        public void CurrentValidButtonIsSelected()
        {
            
            if (currentValidButton.GetComponent<Button>().IsInteractable())
            {
                currentValidButton.GetComponent<Button>().onClick.Invoke();
                StartCoroutine(LittleAnimationOfButton(currentValidButton));
            }

            //currentValidButton.GetComponent<Button>().onClick.Invoke();
            //StartCoroutine(LittleAnimationOfButton(currentValidButton));



        }

        IEnumerator LittleAnimationOfButton(GameObject button)
        {
            button.GetComponent<Button>().interactable = false;
            yield return new WaitForEndOfFrame();
            button.GetComponent<Button>().interactable = true;
        }

    }
}
