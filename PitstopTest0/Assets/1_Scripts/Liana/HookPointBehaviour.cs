using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookPointBehaviour : MonoBehaviour
{
    public RootBehaviour2 root;
    public GameObject thisOne;
    public bool markSign;

    Renderer myRenderer;
    [SerializeField]
    Color color;
    [SerializeField]
    bool canContinue = true;
    Animator anim;

    private void Start()
    {
        thisOne = this.gameObject;
        myRenderer = GetComponent<Renderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            markSign = false;
            canContinue = true;
            root.mark = false;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if(root.hookpoints[1] == null)
            {
                markSign = false;
            }
        }

        if (root.mark == true)
        {
            markSign = false;
        }

        anim.SetBool("Marked", markSign);
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
            color = myRenderer.material.GetColor("_ColorOutline");
            color.a = 255;
            myRenderer.material.SetColor("_ColorOutline", color);
        }              
    }

    private void OnMouseExit()
    {
        color = myRenderer.material.GetColor("_ColorOutline");
        color.a = 0;
        myRenderer.material.SetColor("_ColorOutline", color);
    }
}