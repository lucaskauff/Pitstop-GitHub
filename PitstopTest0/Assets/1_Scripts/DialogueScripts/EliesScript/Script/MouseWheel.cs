using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pitstop
{
    public class MouseWheel : MonoBehaviour
    {
        public Vector2 origin;
        public bool activated;

        public bool[] elements = new bool[8];
        public Image[] images;

        public GameObject wheel;

        public float angle;

        private Camera cam;

        public GameObject currentValidButton = null;

        private void Awake()
        {
            cam = Camera.main;
        }

        // Start is called before the first frame update
        void Start()
        {
            InitializeImages();
            currentValidButton = images[0].gameObject;
        }

        // Update is called once per frame
        void Update()
        {

            if ((Input.GetKey(KeyCode.LeftControl)))
            {
                Wheel();

                if (Input.GetMouseButtonDown(0)) 
                {
                    CurrentValidButtonIsSelected();
                }
                    
            }
            else
            {
                CloseWheel();
            }


        }

        public void Wheel()
        {
            if (!activated) ActivateWheel();

            SetAngle();

            PickElement();
        }

        public void ActivateWheel()
        {
            origin = cam.ScreenToWorldPoint(Input.mousePosition);
            activated = true;
            wheel.SetActive(true);
            wheel.transform.position = cam.WorldToScreenPoint(origin);
        }

        public void SetAngle()
        {
            if(activated)
            {
                Vector2 direction = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition) - origin;
                angle = Mathf.Atan2(direction.normalized.y, direction.normalized.x) * Mathf.Rad2Deg;
                angle -= 90 / 4 * 3;
                angle = angle >= 0 ? angle : angle + 360;
                angle = angle > 360 ? angle - 360 : angle;
            }
        }

        public void CloseWheel()
        {
            activated = false;
            wheel.SetActive(false);
        }

        public void PickElement()
        {
            if (angle < 45) HandleElements(0);
            else if(angle < 90) HandleElements(1);
            else if (angle < 135) HandleElements(2);
            else if (angle < 180) HandleElements(3);
            else if (angle < 225) HandleElements(4);
            else if (angle < 270) HandleElements(5);
            else if (angle < 315) HandleElements(6);
            else HandleElements(7);
        }

        public void HandleElements(int _index)
        {
            /*
            for (int i = 0; i < elements.Length; i++)
            {
                elements[i] = false;

            }

            elements[_index] = true;
            */
            //images[_index].color = Color.red;
            images[_index].gameObject.GetComponent<Button>().Select();

            currentValidButton = images[_index].gameObject;
        }

        public void InitializeImages()
        {
            images = new Image[8];
            int index = 0;
            foreach (Transform item in wheel.transform)
            {
                images[index] = item.GetComponent<Image>();
                index++;
            }
        }

        public void CurrentValidButtonIsSelected()
        {
            currentValidButton.GetComponent<Button>().onClick.Invoke();

            StartCoroutine(LittleAnimationOfButton(currentValidButton));

        }

        IEnumerator LittleAnimationOfButton(GameObject button)
        {
            button.GetComponent<Button>().interactable = false;
            yield return new WaitForEndOfFrame();
            button.GetComponent<Button>().interactable = true;
        }

        /*
        public int GetSelected()
        {
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i]) return i;
            }

            return 0;
        }
        */
    }
}