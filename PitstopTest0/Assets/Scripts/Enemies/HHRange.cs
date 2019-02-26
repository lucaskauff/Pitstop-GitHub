using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HHRange : MonoBehaviour
{
    public HHBehaviour hammerheadBeh;

    void OnTriggerEnter2D(Collider2D collider)
    {
        //condition not perfect !!!
        if (!hammerheadBeh.isArrived && hammerheadBeh.target == null && (collider.gameObject.name == "Zayn" || collider.gameObject.name == "Native"))
        {
            hammerheadBeh.target = collider.gameObject;
        }
    }
}