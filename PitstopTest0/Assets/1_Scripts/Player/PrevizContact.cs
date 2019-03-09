using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class PrevizContact : MonoBehaviour
    {
        public bool objectShootable = false;

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.name == "ShootableArea" || collision.gameObject.tag == "PressurePlate")
            {
                objectShootable = true;
            }
            else
            {
                objectShootable = false;
            }
        }
    }
}