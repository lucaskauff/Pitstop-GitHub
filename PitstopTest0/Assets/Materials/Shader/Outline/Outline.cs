using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline : MonoBehaviour
{

    Renderer myRenderer;
    [SerializeField] Color color;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            color = myRenderer.material.GetColor("_ColorOutline");
            color.a = 255;
            myRenderer.material.SetColor("_ColorOutline", color);

        }
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