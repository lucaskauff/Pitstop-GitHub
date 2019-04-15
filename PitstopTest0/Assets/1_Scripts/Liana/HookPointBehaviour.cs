using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class HookPointBehaviour : MonoBehaviour
    {
        //MyComponents
        Renderer myRend;
        Animator myAnim;

        //Public
        public RootBehaviour2 root;
        public GameObject thisOne;
        public bool markSign;

        //Serializable
        [SerializeField]
        Color color;
        [SerializeField]
        bool canContinue = true;

        private void Start()
        {
            thisOne = this.gameObject;
            myRend = GetComponent<Renderer>();
            myAnim = GetComponent<Animator>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (root.crys.scannedObject.name == "ScannableRoot")
                {
                    markSign = false;
                    canContinue = true;
                    root.mark = false;
                }                
                else
                {
                    return;
                }
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                if (root.hookpoints[1] == null)
                {
                    markSign = false;
                }
            }

            if (root.mark == true)
            {
                markSign = false;
            }

            myAnim.SetBool("Marked", markSign);
        }

        private void OnMouseOver()
        {
            if (root.pointSelect)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (root.hookpoints[x] == null && canContinue == true)
                    {
                        root.hookpoints[x] = thisOne;
                        canContinue = false;
                        markSign = true;
                    }
                }
            }

            if (root.crys.scannedObject.name == "ScannableRoot")
            {
                color = myRend.material.GetColor("_ColorOutline");
                color.a = 255;
                myRend.material.SetColor("_ColorOutline", color);
            }
        }

        private void OnMouseExit()
        {
            color = myRend.material.GetColor("_ColorOutline");
            color.a = 0;
            myRend.material.SetColor("_ColorOutline", color);
        }
    }
}