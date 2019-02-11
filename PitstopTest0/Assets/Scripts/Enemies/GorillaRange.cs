using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorillaRange : MonoBehaviour
{
    public GorillaBehaviour gorillaBeh;

    void OnTriggerStay2D(Collider2D collider)
    {
        if (!gorillaBeh.isArrived && gorillaBeh.target == null && collider.gameObject.name == "Zayn")
        {
            gorillaBeh.target = collider.gameObject;
        }        
    }
}