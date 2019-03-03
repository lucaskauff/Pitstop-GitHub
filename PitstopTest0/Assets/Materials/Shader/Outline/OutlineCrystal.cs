using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineCrystal : MonoBehaviour
{

    Renderer myRenderer;
    [SerializeField] Color color;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<Renderer>();
    }


    private void OnMouseOver()
    {
            color = myRenderer.material.GetColor("_ColorOutline");
            color.a = 255;
            myRenderer.material.SetColor("_ColorOutline", color);
    }

    private void OnMouseExit()
    {
            color = myRenderer.material.GetColor("_ColorOutline");
            color.a = 0;
            myRenderer.material.SetColor("_ColorOutline", color);

    }
}