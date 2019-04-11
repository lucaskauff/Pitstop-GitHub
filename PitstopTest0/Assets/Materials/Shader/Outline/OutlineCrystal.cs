using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class OutlineCrystal : MonoBehaviour
    {
        //My Components
        Renderer myRenderer;

        //Serializable
        [SerializeField] Color color = default;
        [SerializeField] ScannableObjectBehaviour scannableObjectBehaviour = default;

    void Start()
        {
            myRenderer = GetComponent<Renderer>();
        }

        private void OnMouseOver()
        {
            if (scannableObjectBehaviour.isScannable)
            {
                SetOutline(255);
            }
        }

        private void OnMouseExit()
        {
            SetOutline(0);
        }

        void SetOutline(int alphaAmount)
        {
            color = myRenderer.material.GetColor("_ColorOutline");
            color.a = alphaAmount;
            myRenderer.material.SetColor("_ColorOutline", color);
        }
    }
}