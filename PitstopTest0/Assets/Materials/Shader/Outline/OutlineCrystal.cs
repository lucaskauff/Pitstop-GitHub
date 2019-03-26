using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class OutlineCrystal : MonoBehaviour
    {
        Renderer myRenderer;
        [SerializeField] Color color;
        [SerializeField] ScannableObjectBehaviour scannableObjectBehaviour;

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