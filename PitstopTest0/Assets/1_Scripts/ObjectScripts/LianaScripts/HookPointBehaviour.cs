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
        public bool markSign = false;

        //Private
        bool canContinue = true;

        //SoundManagement
        AudioSource soundOfSelection;

        private void Start()
        {
            inputManager = GameManager.Instance.inputManager;
            soundOfSelection = GameObject.FindGameObjectWithTag("SoundLianaSelection").GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (inputManager.onLeftClick && associatedRoot.crystalController.scannedObject != null)
            {
                if (associatedRoot.crystalController.scannedObject.tag == "ObjectRoot")
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
                for (int i = 0; i < associatedRoot.hookpoints.Length; i++)
                {
                    if (associatedRoot.hookpoints[i] == null && canContinue == true)
                    {
                        associatedRoot.hookpoints[i] = gameObject;

                        soundOfSelection.pitch += 0.2f;
                        soundOfSelection.Play();

                        canContinue = false;
                        markSign = true;
                    }
                }
            }
            if (associatedRoot.crystalController.scannedObject != null)
            {
                if (associatedRoot.crystalController.scannedObject.tag == "ObjectRoot")
                {
                    outlineColorOnMouseOver = myRend.material.GetColor("_ColorOutline");
                    outlineColorOnMouseOver.a = 255;
                    myRend.material.SetColor("_ColorOutline", outlineColorOnMouseOver);
                }
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