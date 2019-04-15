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
        public bool plateDown = false;

        //Private
        public GameObject objectOnPlate;

        private void Start()
        {
            myCollider = GetComponent<Collider2D>();
            myAnim = GetComponent<Animator>();
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (objectOnPlate != null && collision.gameObject != objectOnPlate)
            {
                return;
            }
            else if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "ObjectRock")
            {
                objectOnPlate = collision.gameObject;
            }
            else
            {
                objectOnPlate = null;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject == objectOnPlate)
            {
                objectOnPlate = null;
            }
        }

        private void LateUpdate()
        {
            if (objectOnPlate == null)
            {
                plateDown = false;
            }
            else
            {
                plateDown = true;
            }

            myAnim.SetBool("PlateDown", plateDown);
        }
    }
}