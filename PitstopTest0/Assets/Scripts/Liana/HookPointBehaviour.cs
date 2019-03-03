using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookPointBehaviour : MonoBehaviour
{
    public RootBehaviour2 root;
    public GameObject thisOne;
    public bool markSign;

    [SerializeField]
    bool canContinue = true;

    private void Start()
    {
        thisOne = this.gameObject;
    }

    private void Update()
    {    
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            canContinue = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            markSign = false;
        }
    }

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
                    markSign = true;
                }        
            }
        }
    }
}