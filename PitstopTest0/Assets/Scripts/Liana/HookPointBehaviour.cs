using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookPointBehaviour : MonoBehaviour
{
    public RootBehaviour root;

    private void OnMouseOver()
    {
        root.target = this.gameObject;
    }

    private void OnMouseExit()
    {
        root.target = null;
    }
}