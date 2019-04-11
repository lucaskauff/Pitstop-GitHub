using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class PressurePlateBehaviour : MonoBehaviour
    {
        //My Components
        Collider2D myCollider;
        Animator myAnim;

        //Public
        [HideInInspector] public bool plateDown = false;

        //Private
        GameObject objectOnPlate;

        private void Start()
        {
            myCollider = GetComponent<Collider2D>();
            myAnim = GetComponent<Animator>();
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if ((collision.gameObject.name == "Zayn" || collision.gameObject.tag == "ObjectRock") && objectOnPlate == null)
            {
                objectOnPlate = collision.gameObject;
                plateDown = true;
                myAnim.SetBool("PlateDown", plateDown);
            }
        }
        
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject == objectOnPlate)
            {
                objectOnPlate = null;
                plateDown = false;
                myAnim.SetBool("PlateDown", plateDown);
            }
        }
    }
}