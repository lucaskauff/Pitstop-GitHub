using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class PressurePlateBehaviour : MonoBehaviour
    {
        [Header("My components")]
        [SerializeField] Collider2D myCollider = default;
        [SerializeField] Animator myAnim = default;

        //Public
        [HideInInspector] public bool plateDown = false;
        [HideInInspector] public GameObject objectOnPlate;

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