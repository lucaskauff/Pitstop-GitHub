using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class HookPointBehaviour : MonoBehaviour
    {
        //GameManager
        InputManager inputManager;

        [Header("My Components")]
        [SerializeField] Renderer myRend = default;
        [SerializeField] Animator myAnim = default; 

        [SerializeField] RootBehaviour2 associatedRoot = default;
        [SerializeField] Color outlineColorOnMouseOver = default;

        //Public
        [HideInInspector] public bool markSign = false;

        //Private
        bool canContinue = true;

        private void Start()
        {
            inputManager = GameManager.Instance.inputManager;
        }

        private void Update()
        {
            if (inputManager.onLeftClick)
            {
                if (associatedRoot.crys.scannedObject.tag == "ObjectRoot")
                {
                    markSign = false;
                    canContinue = true;
                    associatedRoot.mark = false;
                }                
                else
                {
                    return;
                }
            }

            if (inputManager.onLeftClickReleased)
            {
                if (associatedRoot.hookpoints[1] == null)
                {
                    markSign = false;
                }
            }

            if (associatedRoot.mark == true)
            {
                markSign = false;
            }

            myAnim.SetBool("Marked", markSign);
        }

        private void OnMouseOver()
        {
            if (associatedRoot.pointSelect)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (associatedRoot.hookpoints[x] == null && canContinue == true)
                    {
                        associatedRoot.hookpoints[x] = gameObject;
                        canContinue = false;
                        markSign = true;
                    }
                }
            }

            if (associatedRoot.crys.scannedObject.tag == "ObjectRoot")
            {
                outlineColorOnMouseOver = myRend.material.GetColor("_ColorOutline");
                outlineColorOnMouseOver.a = 255;
                myRend.material.SetColor("_ColorOutline", outlineColorOnMouseOver);
            }
        }

        private void OnMouseExit()
        {
            outlineColorOnMouseOver = myRend.material.GetColor("_ColorOutline");
            outlineColorOnMouseOver.a = 0;
            myRend.material.SetColor("_ColorOutline", outlineColorOnMouseOver);
        }
    }
}