using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pitstop
{
    public class LUD_TestButton_OverrideWithMouse : MonoBehaviour
    {
        //SerializeField
        [SerializeField]
        Sprite spriteUnselected;
        [SerializeField]
        Sprite spriteSelected;

        //Private
        bool isMouseOver = false;


        // Start is called before the first frame update
        void Start()
        {
            isMouseOver = false;
        }

        // Update is called once per frame
        void Update()
        {
            //this.GetComponent<Image>().sprite = spriteSelected;
        }

        private void OnMouseEnter()
        {
            isMouseOver = true;
            Debug.Log("Mouse enter");
            this.GetComponent<Image>().sprite = spriteSelected;
        }

        private void OnMouseExit()
        {
            isMouseOver = false;
            Debug.Log("Mouse exit");
            this.GetComponent<Image>().sprite = spriteUnselected;
        }

    }
}