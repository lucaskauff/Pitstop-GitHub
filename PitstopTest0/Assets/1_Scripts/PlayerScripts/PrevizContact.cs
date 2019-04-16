using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class PrevizContact : MonoBehaviour
    {
        [HideInInspector] public bool objectShootable = false;

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "ShootableArea" || collision.gameObject.tag == "PressurePlate" || collision.gameObject.tag == "Player")
            {
                objectShootable = true;
            }
            else
            {
                objectShootable = false;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "ShootableArea" || collision.gameObject.tag == "PressurePlate" || collision.gameObject.tag == "Player")
            {
                objectShootable = false;
            }
        }
    }
}