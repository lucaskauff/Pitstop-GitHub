using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateBehaviour : MonoBehaviour
{
    Animator myAnim;

    private bool plateDown = false;

    private void Start()
    {
        myAnim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("enter");
        if (collision.gameObject.name == "Zayn" || collision.gameObject.tag == "ObjectRock")
        {            
            plateDown = true;
            myAnim.SetBool("PlateDown", plateDown);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Zayn" || collision.gameObject.tag == "ObjectRock")
        {
            plateDown = false;
            myAnim.SetBool("PlateDown", plateDown);
        }
    }
}