using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookPointBehaviour : MonoBehaviour
{
    public RootBehaviour2 root;
    public GameObject thisOne;


    private void Start()
    {
        thisOne = this.gameObject;
    }

    bool canContinue = true;

    private void OnMouseOver()
    {
        if (root.pointSelect)
        {       

            Debug.Log("Selecting");

            for (int x = 0; x < 3; x++)
            {
                if (root.hookpoints[x] == null && canContinue == true)
                {
                    root.hookpoints[x] = thisOne;
                    canContinue = false;
                }
            }
        }
    }
}