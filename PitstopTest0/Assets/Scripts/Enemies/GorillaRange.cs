using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorillaRange : MonoBehaviour
{
    public GorillaBehaviour gorillaBeh;

    void OnTriggerStay2D(Collider2D collider)
    {
        //Add Native as potential target here !
        if (!gorillaBeh.isArrived && gorillaBeh.target == null && (collider.gameObject.name == "Zayn" || collider.gameObject.name == "Native"))
        {
            gorillaBeh.target = collider.gameObject;
        }        
    }
}