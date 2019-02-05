using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionUIElement : MonoBehaviour
{
    private Animator myAnim;
    public bool isMouseOver = false;

    private void Start()
    {
        myAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        myAnim.SetBool("IsMouseOver", isMouseOver);
    }

    private void OnMouseOver()
    {
        isMouseOver = true;
    }

    private void OnMouseExit()
    {
        isMouseOver = false;
    }
}