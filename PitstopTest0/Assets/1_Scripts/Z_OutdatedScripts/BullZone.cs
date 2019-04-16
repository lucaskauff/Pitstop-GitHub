using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullZone : MonoBehaviour
{
    //[SerializeField] GameObject bull = default;
    [SerializeField]
    BullController bullControl = default;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name != "Bull")
        {
            bullControl.target = other.gameObject;
        }
    }
}